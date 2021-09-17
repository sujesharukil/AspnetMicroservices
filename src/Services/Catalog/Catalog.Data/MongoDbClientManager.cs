using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data
{
    internal static class MongoDbClientManager
    {
        /// <summary>
        /// The mongo clients
        /// </summary>
        private static readonly ConcurrentDictionary<String, IMongoClient> _mongoClients = new();
        /// <summary>
        /// The mongo databases
        /// </summary>
        private static readonly ConcurrentDictionary<String, IMongoDatabase> _mongoDatabases = new();


        /// <summary>
        /// Gets or Sets a new MongoClient for the server address
        /// </summary>
        /// <param name="tenantCode"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IMongoDatabase Open(string tenantCode, string connectionString)
        {
            if (_mongoDatabases.TryGetValue(tenantCode, out var mongoDatabase))
            {
                return mongoDatabase;
            }
            var connection = new ConnectionString(connectionString);
            var dbName = connection.DatabaseName;

            string connectionStringWithoutDbName = string.Join(",", connection.Hosts.Select(s => s.ToString()));

            if (_mongoClients.TryGetValue(connectionStringWithoutDbName, out var mongoClient))
            {
                mongoDatabase = mongoClient.GetDatabase(dbName);
                _mongoDatabases.TryAdd(tenantCode, mongoDatabase);
                return mongoDatabase;
            }
            var settings = MongoClientSettings.FromConnectionString(connectionString);

            static void socketConfigurator(Socket s)
            {
                s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            }

            settings.ClusterConfigurator = builder => builder
                .ConfigureTcp(tcp => tcp.With(socketConfigurator: (Action<Socket>)socketConfigurator));

            mongoClient = new MongoClient(settings);
            mongoDatabase = mongoClient.GetDatabase(dbName);
            _mongoClients.TryAdd(connectionStringWithoutDbName, mongoClient);
            _mongoDatabases.TryAdd(tenantCode, mongoDatabase);
            return mongoDatabase;
        }

    }
}
