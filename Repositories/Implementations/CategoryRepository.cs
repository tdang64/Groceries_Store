using Amazon.DynamoDBv2.DataModel;
using Group4_Project.Models;
using Group4_Project.Repository.Interfaces;

namespace Group4_Project.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDynamoDBContext _context;

        public CategoryRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var scan = _context.ScanAsync<Category>(new List<ScanCondition>());
            return await scan.GetNextSetAsync();
        }

        public async Task<Category?> GetByIdAsync(string id)
        {
            return await _context.LoadAsync<Category>(id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.SaveAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _context.SaveAsync(category);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<Category>(id);
        }
    }
}
