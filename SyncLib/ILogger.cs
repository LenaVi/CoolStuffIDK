using System.IO;
using System.Windows.Controls;

namespace SyncLib
{
    public interface ILogger : IVisitor
    {
        TextBox Writer { get; }
        void Log();
    }
}