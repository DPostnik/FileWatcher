using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using log4net;
using project.DA;
using project.Models.Props;

namespace project.Service
{
    public class FileHandler
    {
        object obj = new object();
        private CSVParser _parser;
        private readonly log4net.ILog _log;
        private string _watchingPath;
        private SQLNoteRepository _sqlNoteRepository;

        public FileHandler(FileWatcher fileWatcher)
        {
            _watchingPath = fileWatcher.WatchingPath;
            Properties properties = new Properties();
            _log = LogManager.GetLogger(properties.GetPropertyValueByName("log_path"), typeof(FileHandler));
            fileWatcher.Changed += OnChanged;
            fileWatcher.Created += OnCreated;
            fileWatcher.Deleted += OnDeleted;
            fileWatcher.Renamed += OnRenamed;
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            var thread = new Thread(() =>
            {
                string fileEvent = "создан";
                string filePath = e.FullPath;
                var notes = _parser.Parse(_watchingPath);
                using (_sqlNoteRepository = new SQLNoteRepository())
                {
                    _sqlNoteRepository.Create(notes);
                }
                File.Delete(filePath);
                RecordEntry(fileEvent,filePath);
            });
            thread.Start();
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                _log.Info($"File {filePath} was {fileEvent}");
            }
        }
    }
}
