using Amazon.DynamoDBv2.DataModel;
using Group4_Project.Models;
using Group4_Project.Repository.Interfaces;

namespace Group4_Project.Repository.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IDynamoDBContext _context;

        public SupplierRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            var scan = _context.ScanAsync<Supplier>(new List<ScanCondition>());
            return await scan.GetNextSetAsync();
        }

        public async Task<Supplier?> GetByIdAsync(string id)
        {
            return await _context.LoadAsync<Supplier>(id);
        }

        public async Task AddAsync(Supplier supplier)
        {
            await _context.SaveAsync(supplier);
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            await _context.SaveAsync(supplier);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<Supplier>(id);
        }
    }
}
