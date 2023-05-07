using Core.Data;
using Core.Models;
using Core.Services.Interface;

namespace Core.Services
{
    public class TmpBalanceService : ITmpBalanceService
    {
        private readonly DatabaseContext _context;

        public TmpBalanceService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<TmpBalance> tmpBalances)
        {
            await _context.TmpBalances.AddRangeAsync(tmpBalances);
            await _context.SaveChangesAsync();
        }
    }
}
