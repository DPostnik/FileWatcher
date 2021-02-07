using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using project.Service;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);
            FileWatcher fileWatcher = new FileWatcher();
            fileWatcher.Start();
            FileHandler fileHandler = new FileHandler(fileWatcher);
            while (!stoppingToken.IsCancellationRequested)
            {
                
            }
        }

    }
}
