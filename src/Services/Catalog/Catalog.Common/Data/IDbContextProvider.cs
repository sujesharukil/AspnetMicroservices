using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Common.Data
{
    public interface IDbContextProvider<T>
    {
        public IMongoDatabase MongoDatabase { get; }
        public IMongoCollection<T> MongoCollection { get; }
    }
}
