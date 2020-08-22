using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Events.Data.Models;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

namespace Events.Data.Services
{
    public class EventsFeedService
    {
        private readonly string _FeedUri;
        public EventsFeedService(string feedUri)
        {
            _FeedUri = feedUri;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var eventItems = new List<Event>();
            using (var xmlReader = XmlReader.Create(_FeedUri, new XmlReaderSettings() { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);
                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        ISyndicationItem item = await feedReader.ReadItem();
                        eventItems.Add(item.ConvertToEvent());
                    }
                }
            }
            return eventItems.OrderByDescending(p => p.Title);
        }
    }

    public static class SyndicationExtensions
    {
        public static Event ConvertToEvent(this ISyndicationItem item)
        {
            return new Event
            {
                Title = item.Title,
                Description = item.Description,
                Category = item.Categories.First().Name,
                Date = item.Published.Date
            };
        }
    }
}
