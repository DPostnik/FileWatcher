using NLog;
using project.Service;
using System;
using System.Linq;

namespace project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogManager.LoadConfiguration("nlog.config");
            var log = LogManager.GetCurrentClassLogger();
            log.Debug("Starting up");
            FileWatcher fileWatcher = new FileWatcher();
            fileWatcher.Start();
            FileHandler fileHandler = new FileHandler(fileWatcher);
            Console.WriteLine("Press 'q' to quit the sample.");
            while (Console.Read() != 'q') ;
            log.Debug("Shutting down");
        }
    }
}
