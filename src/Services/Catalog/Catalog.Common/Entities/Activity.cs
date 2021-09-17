using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Common.Entities
{
    public class Activity
    {
        public Activity(
            string id,
            string title,
            DateTime date,
            string description,
            string category,
            string city,
            string venue)
        {
            Id = id;
            Title = title;
            Date = date;
            Description = description;
            Category = category;
            City = city;
            Venue = venue;
        }

        public string Id { get; private set; }
        public string Title { get; }
        public DateTime Date { get; }
        public string Description { get; }
        public string Category { get; }
        public string City { get; }
        public string Venue { get; }
    }
}
