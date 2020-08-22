using MongoDB.Driver;

namespace Events.Data
{
    public class ContextFactory
    {
        private IDataCredentials Credentials { get; }
        public ContextFactory(IDataCredentials credentials)
        {
            this.Credentials = credentials;
        }

        public IMongoDatabase Create()
        { 
            var client = new MongoClient($"mongodb://{this.Credentials.UserId}:{this.Credentials.Password}@{this.Credentials.Host}");

            return client.GetDatabase(this.Credentials.Database);
        }
    }
}
