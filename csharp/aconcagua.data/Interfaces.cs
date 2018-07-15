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

    public class TimeseriesKey : IEquatable<TimeseriesKey>
    {
        public string Source { get; }
        public string Key { get; }

        public TimeseriesKey(string key)
        {
            Source = key;
            Key = key;
        }

        public bool Equals(TimeseriesKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Source, other.Source) && string.Equals(Key, other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TimeseriesKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) ^ (Key != null ? Key.GetHashCode() : 0);
            }
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
