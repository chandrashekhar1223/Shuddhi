namespace Shuddhi
{
    public static class FileLogger
    {

        private static readonly string logFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        private static readonly string logFile =
            Path.Combine(logFolder, $"error-{DateTime.Now:yyyy-MM-dd}.txt");

        public static void LogError(Exception ex)
        {
            try
            {
                // Ensure folder exists
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }

                string message =
                    "----------------------------------------" + Environment.NewLine +
                    "Time: " + DateTime.Now + Environment.NewLine +
                    "Message: " + ex.Message + Environment.NewLine +
                    "StackTrace: " + ex.StackTrace + Environment.NewLine;

                File.AppendAllText(logFile, message);
            }
            catch
            {
                // avoid crash if logging fails
            }
        }

        public static void LogInfo(string message)
        {
            try
            {
                // Ensure folder exists
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }

                string logMessage =
                    "----------------------------------------" + Environment.NewLine +
                    "Time: " + DateTime.Now + Environment.NewLine +
                    "Message: " + message + Environment.NewLine;

                File.AppendAllText(logFile, logMessage);
            }
            catch
            {
                // Avoid crash if logging fails
            }
        }
    }
}
