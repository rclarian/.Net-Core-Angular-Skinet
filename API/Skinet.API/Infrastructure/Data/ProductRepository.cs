using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _dbContext;

        public ProductRepository(StoreContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product != null) 
            {
                return product;
            }
            return null;
        }
            
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _dbContext.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _dbContext.ProductTypes.ToListAsync();
        }
    }
}
