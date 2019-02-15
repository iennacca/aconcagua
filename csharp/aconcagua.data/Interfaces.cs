using System;
using System.Collections.Generic;
using System.Linq;

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

    public enum FrequencyIndicator
    {
        Annual = 'A',
        Quarterly = 'Q',
        Monthly = 'M'
    }

    public struct TimePeriodSpan
    {
        public DateTime Start;
        public DateTime End;
        public IEnumerable<FrequencyIndicator> Frequencies;
    }

    public interface ITimeseriesMetadata
    {
        TimeseriesSourceKey SourceKey { get; }
        TimeseriesKey SeriesKey { get; }
        IDictionary<string, string> Metadata { get; }
    }

    public interface ITimeseriesObservations
    {
        TimeseriesSourceKey SourceKey { get; }
        TimeseriesKey SeriesKey { get; }
        IDictionary<string, double> Observations { get; }
    }

    public interface ITimeseriesSource
    {
        string SchemeType { get; }
        TimeseriesSourceKey SourceKey { get; }
        IQueryable<TimeseriesKey> GetSeriesKeys(IReadOnlyDictionary<string, string> filters);
        IQueryable<ITimeseriesMetadata> GetMetadata(IEnumerable<TimeseriesKey> seriesKeys, IEnumerable<string> headerList);
        IQueryable<ITimeseriesObservations> GetObservations(IEnumerable<TimeseriesKey> seriesKeys, string frequencies);
    }
}