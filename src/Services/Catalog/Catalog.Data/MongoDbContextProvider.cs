using Catalog.Common.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class MongoDbContextProvider<T> : IDbContextProvider<T>
    {
        
        public MongoDbContextProvider(
            string tenant,
            string connectionString,
            string collectionName)
        {
            MongoDatabase = MongoDbClientManager.Open(tenant, connectionString);
            MongoCollection = MongoDatabase.GetCollection<T>(collectionName);
        }

        public IMongoDatabase MongoDatabase { get; }
        public IMongoCollection<T> MongoCollection { get; }
    }
}
