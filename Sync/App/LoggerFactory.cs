using SyncLib;
using System.IO;
using System.Windows.Controls;

namespace App
{
    public class LoggerFactory
    {
        public static ILogger GetLogger(LoggerType logType, TextBox writer)
        {
            switch (logType)
            {
                case LoggerType.Silent:
                    return new SilentLoggerVisitor();
                case LoggerType.Verbose:
                    return new VerboseLoggerVisitor(writer);
                default:
                    return new SummaryLoggerVisitor(writer);
            }
        }
    }
}
