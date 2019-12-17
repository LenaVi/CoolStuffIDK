using SyncLib;
using System.IO;
using System.Windows.Controls;

namespace App
{
    class SilentLoggerVisitor : ILogger
    {
        public SilentLoggerVisitor() { }
        public TextBox Writer { get; }

        public void Log() { }

        public void Visit(DifferentContentConflict conflict) { }

        public void Visit(ExistDirectoryConflict conflict) { }

        public void Visit(ExistFileConflict conflict) { }

        public void Visit(NoExistDirectoryConflict conflict) { }

        public void Visit(NoExistFileConflict conflict) { }
    }
}
