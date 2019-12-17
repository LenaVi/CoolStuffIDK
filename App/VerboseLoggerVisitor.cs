using SyncLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace App
{
    public class VerboseLoggerVisitor : ILogger
    {
        public VerboseLoggerVisitor(TextBox writer)
        {
            Writer = writer;
        }

        public TextBox Writer { get; }

        private readonly List<string> logs = new List<string>();

        public void Log()
        {
            logs.ForEach(message => Writer.AppendText(message + Environment.NewLine));
        }

        public void Visit(DifferentContentConflict conflict)
        {
            logs.Add($"Файл {conflict.DestinationPath} изменен в соответсвии с {conflict.SourcePath}");
        }

        public void Visit(ExistDirectoryConflict conflict)
        {
            logs.Add($"Директория {conflict.DirectoryPath} удалена");
        }

        public void Visit(ExistFileConflict conflict)
        {
            logs.Add($"Файл {conflict.FilePath} удален");
        }

        public void Visit(NoExistDirectoryConflict conflict)
        {
            logs.Add($"Создание директории {conflict.DirectoryPath}");
        }

        public void Visit(NoExistFileConflict conflict)
        {
            logs.Add($"Файл {conflict.DestinationPath} скопирован из {conflict.SourcePath}");
        }
    }
}
