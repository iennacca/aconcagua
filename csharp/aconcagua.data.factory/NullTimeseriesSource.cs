using System;
using System.Collections.Generic;
using System.Linq;

namespace aconcagua.data.factory
{
    public class NullTimeseriesSource : ITimeseriesSource
    {
        private const string _schemeType = "null";
        private readonly Dictionary<TimeseriesKey, NullTimeseries> _seriesList;

        public string SchemeType { get; } = _schemeType;
        public TimeseriesSourceKey SourceKey { get; }
        #region Initialization

        private NullTimeseriesSource(TimeseriesSourceKey sourceKey)
        {
            SourceKey = sourceKey;
            _seriesList = new Dictionary<TimeseriesKey, NullTimeseries>();
        }

        public static bool TryCreate(TimeseriesSourceKey sourceKey, out ITimeseriesSource timeseriesSource)
        {
            timeseriesSource = null;
            if (String.Equals(sourceKey.Key.Scheme, _schemeType))
                timeseriesSource = new NullTimeseriesSource(sourceKey);
            return (timeseriesSource != null);
        }

        #endregion

        #region Execution

        public IEnumerable<ITimeseries> GetMetadata(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
            var seriesList = new List<ITimeseries>();
            var enumerable = headerList as string[] ?? headerList.ToArray();

            foreach (var seriesKey in seriesKeys)
            {
                if (!_seriesList.ContainsKey(seriesKey))
                    _seriesList.Add(seriesKey, new NullTimeseries(SourceKey, seriesKey, enumerable));

                seriesList.Add(_seriesList[seriesKey]);
            }

            return seriesList;
        }

        public IEnumerable<ITimeseries> GetObservations(IEnumerable<TimeseriesKey> seriesKeys, TimeSpan span)
        {
            throw new NotImplementedException();
        }

        #endregion

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
            {
                var observationValue = new Random().NextDouble();
                _headerData.Add(header, $"{SourceKey.Key}|{SeriesKey.Key}|{header}|{observationValue}");
            }
        }
    }
}
