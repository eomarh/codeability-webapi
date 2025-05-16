using APIChallenge.Models;

namespace APIChallenge.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        // private readonly ApplicationDbContext _context;

        // public WarehouseRepository(ApplicationDbContext context)
        // {
        //     _context = context;
        // }
        public IEnumerable<CapacityRecord> GetCapacityRecords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CapacityRecord> GetCapacityRecords(Func<CapacityRecord, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductRecord> GetProductRecords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductRecord> GetProductRecords(Func<ProductRecord, bool> filter)
        {
            throw new NotImplementedException();
        }

        public void SetCapacityRecord(int productId, int capacity)
        {
            throw new NotImplementedException();
        }

        public void SetProductRecord(int productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}