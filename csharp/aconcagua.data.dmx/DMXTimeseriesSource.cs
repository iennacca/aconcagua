using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
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

        #region Execution

        public IQueryable<ITimeseries> GetObservations(IEnumerable<TimeseriesKey> seriesKeys, TimeSpan span)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ITimeseries> GetMetadata(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
            var hdrs = headerList as string[] ?? headerList.ToArray();
            var seriesList = new List<ITimeseries>();

            var command = _connection.CreateCommand();
            command.CommandText = createQuery(seriesKeys, hdrs);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var headerData = new Dictionary<string, string>();
                var seriesKey = new TimeseriesKey(reader.GetString(0));

                foreach (var i in Enumerable.Range(1,reader.FieldCount - 1))
                {
                    var columnName = reader.GetName(i);
                    headerData[columnName] = reader[i].ToString();
                }
                seriesList.Add(new DMXTimeseries(SourceKey, seriesKey, headerData));
            }

            return seriesList.AsQueryable();
        }

        private string createQuery(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
            var strHeaders = headerList.Aggregate((a, s) => $"{a}, {s}");
            var strSeriesKeys = seriesKeys.ToTimeseriesKeyString(QuotedSplitStringOptions.Quoted);
            var selectQuery = $"SELECT onm.oname, {strHeaders} FROM (tbl_ONames onm" +
                              " INNER JOIN tbl_ODPs odp ON onm.OID = odp.OID)";
            var whereClause = $" WHERE onm.OName in ({strSeriesKeys})";

            return selectQuery + whereClause;
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

        private class DMXTimeseries : ITimeseries
        {
            public TimeseriesSourceKey SourceKey { get; }
            public TimeseriesKey SeriesKey { get; }
            public IReadOnlyDictionary<string, string> HeaderData { get; }

            internal DMXTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey,
                IReadOnlyDictionary<string, string> headerData)
            {
                SourceKey = sourceKey;
                SeriesKey = seriesKey;
                HeaderData = headerData;
            }
        }

    }

}
