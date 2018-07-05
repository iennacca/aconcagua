using System;

namespace aconcagua.data
{
    public class UnknownTimeseriesSourceException : Exception
    {
        public override string Message { get; } = "Unknown timeseries source.";
    }
}
