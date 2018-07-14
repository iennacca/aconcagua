using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace aconcagua.data
{
    internal class NullTimeseriesSource : ITimeseriesSource
    {
        private readonly Dictionary<TimeseriesKey, NullTimeseries> _seriesList;

        private NullTimeseriesSource(TimeseriesSourceKey sourceKey)
        {
            SourceKey = sourceKey;
            _seriesList = new Dictionary<TimeseriesKey, NullTimeseries>();
        }

        public const string SchemeType = "null";
        public bool AutoCreate { get; set; }

        public TimeseriesSourceKey SourceKey { get; }

        public IEnumerable<ITimeseries> Get(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
            foreach (var seriesKey in seriesKeys)
            {
                _seriesList.Add(seriesKey, new NullTimeseries(SourceKey, seriesKey, headerList));
            }

            return _seriesList.Values;
        }

        public static bool TryCreate(TimeseriesSourceKey sourceKey, out ITimeseriesSource timeseriesSource)
        {
            timeseriesSource = null;
            if (String.Equals(sourceKey.Key.Scheme,NullTimeseriesSource.SchemeType))
                timeseriesSource = new NullTimeseriesSource(sourceKey);
            return (timeseriesSource != null);
        }
    }

    internal class NullTimeseries : ITimeseries
    {
        public TimeseriesSourceKey SourceKey { get; }
        public TimeseriesKey SeriesKey { get; }
        public IReadOnlyDictionary<string, string> HeaderData => _headerData;

        private readonly Dictionary<string, string> _headerData;

        public NullTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey, IEnumerable<string> headerList)
        {
            SourceKey = sourceKey;
            SeriesKey = seriesKey;
            _headerData = new Dictionary<string, string>();
            foreach (var header in headerList)
                _headerData.Add(header, $"{SourceKey.Key}|{SeriesKey.Key}|{header}|DATA");
        }
    }
}
