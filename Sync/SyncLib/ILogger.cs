using System.IO;

namespace SyncLib
{
    public interface ILogger : IVisitor
    {
        TextWriter Writer { get; }
        void Log();
    }
}