using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        public DMXTimeseriesSource(TimeseriesSourceKey sourceKey)
        {
            SourceKey = sourceKey;
            var strConn = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={sourceKey.Key.LocalPath}";
            _connection = new OleDbConnection(strConn);

            _connection.Open();
        }

        public IEnumerable<ITimeseries> Get(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
            var query = @"select * from tbl_onames";
            var command = _connection.CreateCommand();
            command.CommandText = query;
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var t = new DMXTimeseries();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var v1 = reader.GetValue(i);
                }
            }

            throw new NotImplementedException();
        }

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

        public static bool TryCreate(TimeseriesSourceKey sourceKey, out ITimeseriesSource timeseriesSource)
        {
            timeseriesSource = null;
            if (String.Equals(sourceKey.Key.Scheme, _schemeType))
                timeseriesSource = new DMXTimeseriesSource(sourceKey);
            return (timeseriesSource != null);
        }
    }

    public class DMXTimeseries : ITimeseries
    {
        public TimeseriesSourceKey SourceKey { get; }
        public TimeseriesKey SeriesKey { get; }
        public IReadOnlyDictionary<string, string> HeaderData { get; }
    }
}
