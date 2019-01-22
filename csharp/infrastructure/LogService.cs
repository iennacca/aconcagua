using NLog;

namespace infrastructure
{
    public static class LogService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message)
        {
            _logger.Info(message);
        }
    }
}
