﻿using SyncLib;
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
            logs.Add($"File {conflict.DestinationPath} was changed to {conflict.SourcePath}");
        }

        public void Visit(ExistDirectoryConflict conflict)
        {
            logs.Add($"Directory {conflict.DirectoryPath} was deleted");
        }

        public void Visit(ExistFileConflict conflict)
        {
            logs.Add($"File {conflict.FilePath} was deleted");
        }

        public void Visit(NoExistDirectoryConflict conflict)
        {
            logs.Add($"Directory {conflict.DirectoryPath} was created");
        }

        public void Visit(NoExistFileConflict conflict)
        {
            logs.Add($"File {conflict.DestinationPath} was copied from {conflict.SourcePath}");
        }
    }
}
