using NLog;
using project.DA;
using project.Models.Props;
using System.IO;
using System.Threading;

namespace project.Service
{
    public class FileHandler
    {
        object obj = new object();
        private CSVParser _parser = new CSVParser();
        private readonly Logger _log;
        private string _watchingPath;
        private SQLNoteRepository _sqlNoteRepository;

        public FileHandler(FileWatcher fileWatcher)
        {
            _watchingPath = fileWatcher.WatchingPath;
            Properties properties = new Properties();
            LogManager.LoadConfiguration("nlog.config");
            _log = LogManager.GetCurrentClassLogger();
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
                var notes = _parser.Parse(filePath);
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
                _log.Info($"Файл (путь = {filePath}) был {fileEvent}");
            }
        }
    }
}
