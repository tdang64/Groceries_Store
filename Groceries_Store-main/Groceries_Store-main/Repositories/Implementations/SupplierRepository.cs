using Group4_Project.Data;
using Group4_Project.Models;
using Group4_Project.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Group4_Project.Repository.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly GroceryDbContext _context;

        public SupplierRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task AddAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Suppliers.FindAsync(id);
            if (item != null)
            {
                _context.Suppliers.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
