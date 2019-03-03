using System;
using NLog;

namespace infrastructure.log.nlog
{
    public class LogService : ILogService
    {
        private readonly Logger _logger;

        public LogService(Type classType)
        {
            _logger = LogManager.GetLogger(classType.Name);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}
