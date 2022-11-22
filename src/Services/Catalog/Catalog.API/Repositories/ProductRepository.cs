using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product).ConfigureAwait(false);
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var result = await _catalogContext.Products.DeleteOneAsync<Product>(p => p.Id == productId).ConfigureAwait(false);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await _catalogContext.Products.Find(p => p.Category == categoryName).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _catalogContext.Products.Find(p => p.Name == name).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _catalogContext.Products.ReplaceOneAsync(g => g.Id == product.Id, product).ConfigureAwait(false);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
