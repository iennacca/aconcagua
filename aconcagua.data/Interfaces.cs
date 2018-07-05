using System;
using System.Collections.Generic;

namespace aconcagua.data
{
    public class TimeseriesSourceKey
    {
        public string Source { get; }
        public Uri Key { get; }

        public TimeseriesSourceKey(string sourceKey)
        {
            Source = sourceKey;
            Key = new Uri(sourceKey);
        }
    }

    public class TimeseriesKey
    {
        public string Source { get; }
        public string Key { get; }

        public TimeseriesKey(string sourceKey)
        {
            Source = sourceKey;
            Key = sourceKey;
        }
    }

    public interface ITimeseries
    {
        TimeseriesSourceKey Source { get; }
        TimeseriesKey SeriesName { get; }
        IReadOnlyDictionary<string, string> HeaderData { get; }
    }

    public interface ITimeseriesSource
    {
        TimeseriesSourceKey SourceKey { get; }
        IEnumerable<ITimeseries> Get(IEnumerable<TimeseriesKey> seriesCodeKeys, IEnumerable<string> headerList);
    }
}
