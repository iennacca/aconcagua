using System;
using System.Collections.Generic;

namespace aconcagua.data
{
    public class TimeseriesSourceKey
    {
        public string Source { get; }
        public Uri Key { get; }

        public TimeseriesSourceKey(string key)
        {
            Source = key;
            Key = new Uri(key);
        }
    }

    public class TimeseriesKey
    {
        public string Source { get; }
        public string Key { get; }

        public TimeseriesKey(string key)
        {
            Source = key;
            Key = key;
        }
    }

    public interface ITimeseries
    {
        TimeseriesSourceKey SourceKey { get; }
        TimeseriesKey SeriesKey { get; }
        IReadOnlyDictionary<string, string> HeaderData { get; }
    }

    public interface ITimeseriesSource
    {
        TimeseriesSourceKey SourceKey { get; }
        IEnumerable<ITimeseries> Get(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList);
    }
}
