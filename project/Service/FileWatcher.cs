using project.Models.Props;
using System;
using System.IO;
using System.Threading;

namespace project.Service
{
    public class FileWatcher
    {
            private FileSystemWatcher _watcher;
            object obj = new object();
            private bool _enabled = true;

            public event EventHandler<RenamedEventArgs> Renamed;
            public event EventHandler<FileSystemEventArgs> Changed;
            public event EventHandler<FileSystemEventArgs> Created;
            public event EventHandler<FileSystemEventArgs> Deleted;
            public string WatchingPath { get; }

        public FileWatcher()
            {
                Properties properties = new Properties();
                WatchingPath = properties.GetPropertyValueByName("filepath");
                _watcher = new FileSystemWatcher( WatchingPath)
                {
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size,
                    Filter = "*.csv"
                };
                _watcher.Deleted += Watcher_Deleted;
                _watcher.Created += Watcher_Created;
                _watcher.Changed += Watcher_Changed;
                _watcher.Renamed += Watcher_Renamed;
            }

            public void Start()
            {
                _watcher.EnableRaisingEvents = true;
                while (_enabled)
                {
                    Thread.Sleep(1000);
                }
            }

            public void Stop()
            {
                _watcher.EnableRaisingEvents = false;
                _enabled = false;
            }

            // переименование файлов
            private void Watcher_Renamed(object sender, RenamedEventArgs e)
            {
                Renamed?.Invoke(sender,e);
            }

            // изменение файлов
            private void Watcher_Changed(object sender, FileSystemEventArgs e)
            {
                Changed?.Invoke(sender, e);
            }

            // создание файлов
            private void Watcher_Created(object sender, FileSystemEventArgs e)
            {
                Created?.Invoke(sender, e);
            }

            // удаление файлов
            private void Watcher_Deleted(object sender, FileSystemEventArgs e)
            {
                Deleted?.Invoke(sender,e);
                string fileEvent = "удален";
                string filePath = e.FullPath;
                RecordEntry(fileEvent, filePath);
            }

            private void RecordEntry(string fileEvent, string filePath)
            {
                lock (obj)
                {
                    using (StreamWriter writer = new StreamWriter("D:\\templog.txt", true))
                    {
                        writer.WriteLine(String.Format("{0} файл {1} был {2}",
                            DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                        writer.Flush();
                    }
                }
            }
        }
}