using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Events.Data.Models;
using Events.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Events.Data.Services
{
    public class EventsFeedMonitor : IHostedService
    {
        private IServiceScopeFactory ScopeFactory { get; }
        private EventsFeedService EventsFeedService { get; }
        private Timer Timer { get; set; }
        private readonly ILogger<EventsFeedMonitor> Logger;
        public EventsFeedMonitor(
            ILogger<EventsFeedMonitor> logger,
            IServiceScopeFactory scopeFactory,
            EventsFeedService eventsFeedService)
        {
            this.Logger = logger;
            this.ScopeFactory = scopeFactory;
            this.EventsFeedService = eventsFeedService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.Timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            this.Logger.LogInformation("Timed Hosted Service is working");
            var events = this.EventsFeedService.GetEvents().Result.ToList();

            if (!events.Any())
            {
                return;
            }

            using var scope = this.ScopeFactory.CreateScope();
            var eventRepository = scope.ServiceProvider.GetRequiredService<EventRepository>();
            eventRepository.Delete(Builders<Event>.Filter.Empty);

            // insert new events
            if (events.Any())
            {
                eventRepository.Insert(events);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.Timer?.Dispose();
            this.Logger.LogInformation("Timed Hosted Service is stopping");

            return Task.CompletedTask;
        }
    }
}
