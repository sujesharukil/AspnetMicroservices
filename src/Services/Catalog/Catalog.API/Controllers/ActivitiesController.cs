using Catalog.Common.Entities;
using Catalog.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController(IActivityRepository activityRepository) : base()
        {
            ActivityRepository = activityRepository;
        }

        public IActivityRepository ActivityRepository { get; }

        [HttpGet]
        public Task<IReadOnlyList<Activity>> GetActivities()
        {
            return ActivityRepository.GetAllActivitiesAsync();
        }

        [HttpGet("{id}")]
        public Task<Activity> GetActivity(string id)
        {
            return ActivityRepository.GetActivityAsync(id);
        }
    }
}
