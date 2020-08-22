using System;
using System.Collections.Generic;
using System.Linq;
using Events.Data.Models;
using MongoDB.Driver;

namespace Events.Data.Repositories
{
    public class EventRepository : RepositoryBase<Event>
    {
        private IMongoCollection<Event> Repository { get; }
        public EventRepository(IMongoDatabase database) : base(database)
        {
            this.Repository = this.GetRepository();
        }

        public void Insert(Event e)
        {
            this.Repository.InsertOne(e);
        }

        public void Insert(IEnumerable<Event> events)
        {
            this.Repository.InsertMany(events);
        }

        public IEnumerable<Event> GetAll()
        {
            return this.Repository.Find(_ => true).ToList();
        }

        public Event Get(Guid id)
        {
            var result = this.Repository.Find(Builders<Event>.Filter.Eq("_id", id)).SingleOrDefault();
            return result;
        }

        public IEnumerable<Event> Find(FilterDefinition<Event> filter)
        {
            return this.Repository.Find(filter).ToList();
        }

        public void Update(Event _event)
        {
            this.Repository.ReplaceOne(e => e.Id.Equals(_event.Id), _event);
        }

        public void UpdateMany(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                this.Repository.ReplaceOne(e => e.Id.Equals(@event.Id), @event);
            }
        }

        public void Delete(FilterDefinition<Event> filter)
        {
            this.Repository.DeleteMany(filter);
        }
    }
}
