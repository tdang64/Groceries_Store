using Group4_Project.Data;
using Group4_Project.Models;
using Group4_Project.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Group4_Project.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GroceryDbContext _context;

        public CategoryRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Categories.FindAsync(id);
            if (item != null)
            {
                _context.Categories.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
