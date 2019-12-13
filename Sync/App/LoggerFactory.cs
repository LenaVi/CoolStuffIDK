using SyncLib;
using System.IO;
using System.Windows.Controls;

namespace App
{
    public class LoggerFactory
    {
        public static ILogger GetLogger(LoggerTypes logType, TextBox writer)
        {
            switch (logType)
            {
                case LoggerTypes.Silent:
                    return new SilentLoggerVisitor();
                case LoggerTypes.Verbose:
                    return new VerboseLoggerVisitor(writer);
                default:
                    return new SummaryLoggerVisitor(writer);
            }
        }
    }
}
