using Core.Models;

namespace Core.Services.Interface
{
    public interface ITmpBalanceService
    {
        Task AddRangeAsync(IEnumerable<TmpBalance> tmpBalances);
    }
}
