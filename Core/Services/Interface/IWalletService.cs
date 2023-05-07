using Core.Models;

namespace Core.Services.Interface
{
    public interface IWalletService
    {
        Task<IEnumerable<Wallet>> GetAsync();
        Task<bool> UpdateRangeAsync(IEnumerable<Wallet> updatedWallets);
    }
}
