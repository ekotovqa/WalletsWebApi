using Nethereum.Web3;
using WalletsWebApi.Services.Interface;

namespace WalletsWebApi.Services
{
    public class Web3Service : IWeb3Service
    {
        private readonly ILogger<Web3Service> _logger;
        private readonly IConfiguration _configuration;
        private string _webEndpoint;
        public Web3Service(ILogger<Web3Service> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<decimal?> GetBalance(string web3Address)
        {
            _webEndpoint = _configuration["Web3Endpoint"];
            if (!string.IsNullOrEmpty(_webEndpoint))
            {
                try
                {
                    if (!string.IsNullOrEmpty(web3Address))
                    {
                        if (string.IsNullOrEmpty(_webEndpoint))
                            throw new Exception("Address not installed");
                        var tmpBalance = await new Web3(_webEndpoint).Eth.GetBalance.SendRequestAsync(web3Address);
                        return Web3.Convert.FromWei(tmpBalance.Value);
                    }
                    return null;
                }
                catch (Exception exp)
                {
                    _logger.LogCritical(exp, "Server error. Failed to get balance");
                    return null;
                    throw;
                }
            }
            throw new Exception("Web3Endpoint not installed");
        }
    }
}
