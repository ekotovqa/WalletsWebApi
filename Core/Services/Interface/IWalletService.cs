using Core.Models;

namespace Core.Services.Interface
{
    public interface IWalletService
    {
        Task<IEnumerable<Wallet>> GetAsync();
        Task<bool> UpdateWalletsRangeAsync(IEnumerable<Wallet> updatedWallets);
        Task AddTmpBalancesRangeAsync(IEnumerable<TmpBalance> tmpBalances);
    }
}
