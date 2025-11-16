using Amazon.DynamoDBv2.DataModel;
using Group4_Project.Models;
using Group4_Project.Repository.Interfaces;

namespace Group4_Project.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDynamoDBContext _context;

        public ProductRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var scan = _context.ScanAsync<Product>(new List<ScanCondition>());
            return await scan.GetNextSetAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            return await _context.LoadAsync<Product>(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.SaveAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _context.SaveAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<Product>(id);
        }
    }
}
