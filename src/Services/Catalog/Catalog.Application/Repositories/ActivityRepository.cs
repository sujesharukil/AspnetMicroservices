using Catalog.Common.Data;
using Catalog.Common.Entities;
using Catalog.Common.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IDbContextProvider<Activity> _dbContextProvider;

        public ActivityRepository(IDbContextProvider<Activity> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public Task DeleteActivityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Activity> GetActivityAsync(string id)
        {
            return await _dbContextProvider
                .MongoCollection
                .Find(f => f.Id.Equals(id))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<Activity>> GetAllActivitiesAsync()
        {
            IAsyncCursor<Activity> results = await _dbContextProvider
                .MongoCollection
                .FindAsync<Activity>(new BsonDocument())
                .ConfigureAwait(false);

            return await results.ToListAsync().ConfigureAwait(false);
        }

        public Task InsertActivityAsync(Activity activity)
        {
            throw new NotImplementedException();
        }

        public async Task SeedDataAsync()
        {
            var currentActivitiesCount = await _dbContextProvider
                .MongoCollection
                .CountDocumentsAsync(new BsonDocument())
                .ConfigureAwait(false);

            if (currentActivitiesCount > 0)
            {
                return;
            }

            var activities = new List<Activity>
            {
                new Activity(
                    "",
                    "Past Activity 1",
                    DateTime.Now.AddMonths(-2),
                    "Activity 2 months ago",
                    "drinks",
                    "London",
                    "Pub"),
                new Activity(
                    "",
                    "Past Activity 2",
                    DateTime.Now.AddMonths(-1),
                    "Activity 1 month ago",
                    "culture",
                    "Paris",
                    "Louvre"),
                new Activity(
                    "",
                    "Future Activity 1",
                    DateTime.Now.AddMonths(1),
                    "Activity 1 month in future",
                    "culture",
                    "London",
                    "Natural History Museum"),
                new Activity(
                    "",
                    "Future Activity 2",
                    DateTime.Now.AddMonths(2),
                    "Activity 2 months in future",
                    "music",
                    "London",
                    "O2 Arena"),
            };

            await _dbContextProvider.MongoCollection.InsertManyAsync(activities).ConfigureAwait(false);
        }

        public Task UpdateActivityAsync(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}
