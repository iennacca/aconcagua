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

        public IQueryable<ITimeseriesMetadata> GetMetadata(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList)
        {
            var seriesList = new List<ITimeseriesMetadata>();
            var enumerable = headerList as string[] ?? headerList.ToArray();

            foreach (var seriesKey in seriesKeys)
            {
                if (!_seriesList.ContainsKey(seriesKey))
                    _seriesList.Add(seriesKey, new NullTimeseries(SourceKey, seriesKey, enumerable));

                seriesList.Add(_seriesList[seriesKey]);
            }

            return seriesList.AsQueryable();
        }

        public IQueryable<ITimeseriesObservations> GetObservations(IEnumerable<TimeseriesKey> seriesKeys, string frequencies)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

    internal class NullTimeseries : ITimeseriesMetadata, ITimeseriesObservations
    {
        public TimeseriesSourceKey SourceKey { get; }
        public TimeseriesKey SeriesKey { get; }
        public IDictionary<string, string> Metadata => _metadata;
        public IDictionary<string, double> Observations => _observations;

        private readonly Dictionary<string, string> _metadata = new Dictionary<string, string>();
        private readonly Dictionary<string, double> _observations = new Dictionary<string, double>();

        // TODO [jc]: Create separate initializers for metadata and observations
        public NullTimeseries(TimeseriesSourceKey sourceKey, TimeseriesKey seriesKey, IEnumerable<string> headerList)
        {
            SourceKey = sourceKey;
            SeriesKey = seriesKey;

            foreach (var header in headerList)
            {
                var observationValue = new Random().NextDouble();
                _metadata.Add(header, $"{SourceKey.Key}|{SeriesKey.Key}|{header}|{observationValue}");
            }
        }
    }
}
