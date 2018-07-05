using System;
using System.Collections.Generic;

namespace aconcagua.data
{
    public class NullTimeseriesSource : ITimeseriesSource
    {
        public NullTimeseriesSource(TimeseriesSourceKey sourceKey)
        {
            SourceKey = sourceKey;
        }

        public string SchemeType => "null";
        public bool AutoCreate { get; set; }

        public TimeseriesSourceKey SourceKey { get; }

        public IEnumerable<ITimeseries> Get(IEnumerable<TimeseriesKey> seriesCodeKeys, IEnumerable<string> headerList)
        {
            throw new NotImplementedException();
        }
    }
}
