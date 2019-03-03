using System;

namespace infrastructure
{
    public interface ILogService
    {
        void Trace(string message);
        void Info(string message);
        void Warn(string message);
        void Debug(string message);
        void Error(string message);
    }
}
