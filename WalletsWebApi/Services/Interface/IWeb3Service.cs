namespace WalletsWebApi.Services.Interface
{
    public interface IWeb3Service
    {
        Task<decimal?> GetBalance(string web3Address);
    }
}
