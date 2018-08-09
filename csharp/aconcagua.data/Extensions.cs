using System;
using System.Collections.Generic;
using System.Linq;

namespace aconcagua.data
{
    public enum QuotedSplitStringOptions
    {
        NonQuoted = 0,
        Quoted
    }
 
    public static class Extensions
    {
        private const char Delimiter = ',';

        public static IEnumerable<TimeseriesKey> ToTimeseriesKeys(this string seriesNames)
        {
            return seriesNames.Split(new []{Delimiter}, StringSplitOptions.RemoveEmptyEntries).Select(s => new TimeseriesKey(s)).ToList();
        }

        public static string ToTimeseriesKeyString(this IEnumerable<TimeseriesKey> seriesKeys, QuotedSplitStringOptions quoteOption)
        {
            var q = (quoteOption == QuotedSplitStringOptions.Quoted) ? "'": string.Empty;

            return seriesKeys.Select((s) =>$"{q}{s.Key}{q}").Aggregate((a, s) => $"{a}{Delimiter}{s}");
        }
    }
}
