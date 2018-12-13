using Minuto.Seguros.Domain.Entities;
using Minuto.Seguros.Infra.Data.Connection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace Minuto.Seguros.Infra.Data.Context
{
    public class MongoContext : IDisposable, IConnect
    {                
        protected MongoClient Client { get; private set; }
        protected IMongoDatabase DataBase { get; private set; }

        public IMongoCollection<T> Collection<T>(string CollectionName)
        {
            return DataBase.GetCollection<T>(CollectionName);
        }
        public MongoContext(IConfig config)
        {
            Client = new MongoClient(config.MongoConnectionString);
            DataBase = Client.GetDatabase(config.MongoDatabase);
        }

        public MongoContext(string connectionString, string database)
        {
            Client = new MongoClient(connectionString);
            DataBase = Client.GetDatabase(database);
            Map();
        }

        public IMongoCollection<Feed> Feeds
        {
            get
            {
                return DataBase.GetCollection<Feed>("Feeds");
            }
        }

        private void Map()
        {
            BsonClassMap.RegisterClassMap<Feed>(cm =>
            {
                cm.AutoMap();
            });
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DataBase = null;
                    Client = null;
                }
                disposed = true;
            }
        }
        ~MongoContext()
        {
            Dispose(false);
        }
        private bool disposed = false;
        #endregion Dispose
    }
}
