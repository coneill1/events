using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Events.Data.Models
{
    public class Event
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public Event()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
