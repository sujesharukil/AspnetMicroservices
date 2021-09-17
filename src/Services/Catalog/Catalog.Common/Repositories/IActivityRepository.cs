using Catalog.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Common.Repositories
{
    public interface IActivityRepository
    {
        Task<IReadOnlyList<Activity>> GetAllActivitiesAsync();
        Task<Activity> GetActivityAsync(string id);
        Task InsertActivityAsync(Activity activity);
        Task UpdateActivityAsync(Activity activity);
        Task DeleteActivityAsync(string id);
        Task SeedDataAsync();
    }
}
