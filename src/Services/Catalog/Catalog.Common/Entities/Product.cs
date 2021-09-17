using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Common.Entities
{
    public class Product
    {
        public Product(
            string id,
            string name,
            string category,
            string summary,
            string description,
            string imageFile,
            decimal price)
        {
            Id = id;
            Name = name;
            Category = category;
            Summary = summary;
            Description = description;
            ImageFile = imageFile;
            Price = price;
        }

        public string Id { get; }
        public string Name { get; }
        public string Category { get; }
        public string Summary { get; }
        public string Description { get; }
        public string ImageFile { get; }
        public decimal Price { get; }
    }
}
