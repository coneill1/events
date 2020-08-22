using MongoDB.Driver;

namespace Events.Data.Repositories
{
    public class RepositoryBase<T> where T : class
    {
        private IMongoDatabase Database { get; }
        private string CollectionName { get; }
        public RepositoryBase(IMongoDatabase database)
        {
            this.Database = database;
            this.CollectionName = typeof(T).Name;
        }

        public IMongoCollection<T> GetRepository()
        {
            return this.Database.GetCollection<T>(this.CollectionName);
        }
    }
}
