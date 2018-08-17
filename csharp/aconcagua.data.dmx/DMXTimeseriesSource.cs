using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace aconcagua.data.dmx
{
    public class DMXTimeseriesSource : ITimeseriesSource, IDisposable
    {
        private const string _schemeType = "dmx";
        private readonly IDbConnection _connection;

        public string SchemeType { get; } = _schemeType;
        public TimeseriesSourceKey SourceKey { get; }

        #region Initialization

        public DMXTimeseriesSource(TimeseriesSourceKey sourceKey)
        {
            SourceKey = sourceKey;

            var strConn = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={sourceKey.Key.LocalPath}";
            _connection = new OleDbConnection(strConn);

            _connection.Open();
        }

        public static bool TryCreate(TimeseriesSourceKey sourceKey, out ITimeseriesSource timeseriesSource)
        {
            timeseriesSource = null;
            if (String.Equals(sourceKey.Key.Scheme, _schemeType))
                timeseriesSource = new DMXTimeseriesSource(sourceKey);
            return (timeseriesSource != null);
        }

        #endregion

        #region GetMetadata

        public IQueryable<ITimeseriesMetadata> GetMetadata(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
            var hdrs = headerList as string[] ?? headerList.ToArray();
            var seriesList = new List<ITimeseriesMetadata>();

            var command = _connection.CreateCommand();
            command.CommandText = GetMetadataQuery.Create(seriesKeys, hdrs);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var metadataDictionary = new Dictionary<string, string>();
                var seriesKey = new TimeseriesKey(reader.GetString(0));

                foreach (var i in Enumerable.Range(1,reader.FieldCount - 1))
                {
                    var columnName = reader.GetName(i);
                    metadataDictionary[columnName] = reader[i].ToString();
                }
                seriesList.Add(new DMXTimeseries(SourceKey, seriesKey, metadataDictionary));
            }

            return seriesList.AsQueryable();
        }

        #endregion

        #region GetObservations

        public IQueryable<ITimeseriesObservations> GetObservations(IEnumerable<TimeseriesKey> seriesKeys, string frequencies)
        {
            var seriesList = new Dictionary<TimeseriesKey, ITimeseriesObservations>();

            var command = _connection.CreateCommand();
            command.CommandText = GetObservationsQuery.Create(seriesKeys, frequencies);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var seriesKey = new TimeseriesKey(reader.GetString(0));
                var orowid = reader.GetInt32(1).ToString();
                ITimeseriesObservations series;

                if (!seriesList.ContainsKey(seriesKey))
                {
                    series = new DMXTimeseries(SourceKey, seriesKey);
                    seriesList.Add(seriesKey, series);
                }
                else
                    series = seriesList[seriesKey];

                foreach (var i in Enumerable.Range(2, reader.FieldCount - 2))
                {
                    var columnName = reader.GetName(i);


                    // TODO [jc]: Implement nullable type (Google.WellKnownTypes.Value ?)
                    if (!reader.IsDBNull(i))
                        series.Observations[$"{orowid}:{columnName}"] = reader.GetDouble(i);
                }
            }

            return seriesList.Values.AsQueryable();
        }

        #endregion

        #region IDisposable

        private readonly bool _disposed = false;
        private readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _connection.Close();
                _handle.Dispose();
            }
        }

        #endregion

        private class DMXTimeseries : ITimeseriesMetadata, ITimeseriesObservations
        {
            public TimeseriesSourceKey SourceKey { get; }
            public TimeseriesKey SeriesKey { get; }
            public IDictionary<string, string> Metadata  => _metadata;
            public IDictionary<string, double> Observations => _observations;

            private readonly Dictionary<string, string> _metadata = new Dictionary<string, string>();
            private readonly Dictionary<string, double> _observations = new Dictionary<string, double>();

            internal DMXTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey)
            {
                SourceKey = sourceKey;
                SeriesKey = seriesKey;
            }

            internal DMXTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey, Dictionary<string,string> metadata) : this(sourceKey, seriesKey)
            {
                _metadata = metadata;
            }
        
            internal DMXTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey, Dictionary<string, double> observations) : this(sourceKey,seriesKey)
            {
                _observations = observations;
            }
        }
    }

    internal static class GetObservationsQuery
    {
        public static string Create(IEnumerable<TimeseriesKey> seriesKeys, string frequencies)
        {
            var strOValueFields = CreateOValueFieldListFrom(frequencies);
            var strSeriesKeys = seriesKeys.ToTimeseriesKeyString(QuotedSplitStringOptions.Quoted);
            var selectQuery = $"SELECT onm.oname, t17.ORowID, {strOValueFields} FROM (tbl_ONames onm" +
                              " LEFT JOIN tbl_TSeries17 t17 ON onm.OID = t17.OName)";
            var whereClause = $" WHERE onm.OName in ({strSeriesKeys})";

            return selectQuery + whereClause;
        }

        public static string CreateOValueFieldListFrom(string frequencies)
        {
            var strFields = new List<string>();

            if (frequencies.Contains((char)FrequencyIndicator.Monthly))
                //strFields.Add("OValue1, OValue2, OValue3, OValue4, OValue5, OValue6, OValue7, OValue8, OValue9, OValue10, OValue11, OValue12");
                strFields.Add(CreateOValueFieldNamesFrom(FrequencyIndicator.Monthly));

            if (frequencies.Contains((char)FrequencyIndicator.Quarterly))
                //strFields.Add("OValue13, OValue14, OValue15, OValue16");
                strFields.Add(CreateOValueFieldNamesFrom(FrequencyIndicator.Quarterly));

            if (frequencies.Contains((char)FrequencyIndicator.Annual))
                //strFields.Add("OValue17 AS [A]");
                strFields.Add(CreateOValueFieldNamesFrom(FrequencyIndicator.Annual));

            return strFields.Aggregate((a, s) => $"{a}, {s}");
        }

        public static string CreateOValueFieldNamesFrom(FrequencyIndicator indicator)
        {
            switch (indicator)
            {
                case FrequencyIndicator.Monthly:
                    return Enumerable.Range(1, 12).
                        Select(i => $"OValue{i} AS {(char) indicator}{i}").
                        Aggregate((a, s) => $"{a}, {s}");

                case FrequencyIndicator.Quarterly:
                    return Enumerable.Range(13, 4).
                        Select(i => $"OValue{i} AS {(char) indicator}{i - 12}").
                        Aggregate((a, s) => $"{a}, {s}");

                case FrequencyIndicator.Annual:
                    return $"OValue17 AS A";

                default:
                    return string.Empty;
            }
        }
    }

    internal static class GetMetadataQuery
    {
        public static string Create(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
            var strHeaders = headerList.Aggregate((a, s) => $"{a}, {s}");
            var strSeriesKeys = seriesKeys.ToTimeseriesKeyString(QuotedSplitStringOptions.Quoted);
            var selectQuery = $"SELECT onm.oname, {strHeaders} FROM (tbl_ONames onm" +
                              " INNER JOIN tbl_ODPs odp ON onm.OID = odp.OID)";
            var whereClause = $" WHERE onm.OName in ({strSeriesKeys})";

            return selectQuery + whereClause;
        }
    }

}
