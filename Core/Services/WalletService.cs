using Core.Data;
using Core.Models;
using Core.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly DatabaseContext _context;

        public WalletService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Wallet>> GetAsync() => await _context.Wallets.Include(x => x.TmpBalance).ToListAsync();

        public async Task<bool> UpdateRangeAsync(IEnumerable<Wallet> updatedWallets)
        {
            var wallets = await GetAsync();
            foreach (var wallet in wallets)
            {
                var updatedWallet = updatedWallets.FirstOrDefault(x => x.Id == wallet.Id);
                if (updatedWallet == null) 
                    continue;
                EntityEntry entityEntry = _context.Entry(wallet);
                entityEntry.State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
