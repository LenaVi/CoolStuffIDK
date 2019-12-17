using SyncLib;
using System;
using System.IO;
using System.Windows.Controls;

namespace App
{
    public class SummaryLoggerVisitor : ILogger
    {
        public SummaryLoggerVisitor(TextBox writer)
        {
            Writer = writer;
        }
        public int DeletedCount { get; private set; } = 0;
        public int CopiedCount { get; private set; } = 0;
        public int UpdatedCount { get; private set; } = 0;

        public TextBox Writer { get; }

        public void Visit(DifferentContentConflict conflict) => UpdatedCount++;

        public void Visit(ExistDirectoryConflict conflict) => DeletedCount++;

        public void Visit(ExistFileConflict conflict) => DeletedCount++;

        public void Visit(NoExistDirectoryConflict conflict) => CopiedCount++;
        public void Visit(NoExistFileConflict conflict) => CopiedCount++;

        public void Log()
        {
            Writer.AppendText($"Было скопировано {CopiedCount}" + Environment.NewLine +
                $"Было удалено {DeletedCount}" + Environment.NewLine +
                $"Было обновлено {UpdatedCount}" + Environment.NewLine);
        }
    }
}
