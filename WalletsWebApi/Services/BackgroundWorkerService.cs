using Core.Models;
using Core.Services.Interface;
using System.Diagnostics;
using WalletsWebApi.Services.Interface;

namespace WalletsWebApi.Services
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IWeb3Service _web3Service;
        private readonly IConfiguration _configuration;


        public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IServiceProvider serviceProvider, IWeb3Service web3Service, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _web3Service = web3Service;
            _configuration = configuration;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int taskDelay = 0;
            if (!string.IsNullOrEmpty(_configuration["BackgroundWorkerServiceTaskDelay"]))
                int.TryParse(_configuration["BackgroundWorkerServiceTaskDelay"], out taskDelay);
            while (!stoppingToken.IsCancellationRequested)
            {
                var sw = Stopwatch.StartNew();
                IEnumerable<Wallet> wallets;
                IEnumerable<TmpBalance> tmpBalances = Enumerable.Empty<TmpBalance>();
                _logger.LogInformation($"BackgroundWorkerService started at: {DateTime.Now}");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var walletService = scope.ServiceProvider.GetRequiredService<IWalletService>();
                    var tpmBalanceService = scope.ServiceProvider.GetRequiredService<ITmpBalanceService>();
                    wallets = await walletService.GetAsync();
                    int counter = 0;
                    foreach (var wallet in wallets)
                    {
                        if (wallet.Address != null)
                        {
                            var balance = await _web3Service.GetBalance(wallet.Address);
                            var tmpBalance = new TmpBalance()
                            {
                                Balance = balance,
                                Updated_At = DateTime.UtcNow,
                                WalletId = wallet.Id
                            };
                            if (wallet.TmpBalance == null)
                            {
                                tmpBalances.Append(tmpBalance);
                            }
                            wallet.TmpBalance = tmpBalance;
                        }
                        counter++;
                        if (counter == 100)
                        {
                            await tpmBalanceService.AddRangeAsync(tmpBalances);
                            await walletService.UpdateRangeAsync(wallets);
                            counter = 0;
                        }
                    }
                    await tpmBalanceService.AddRangeAsync(tmpBalances);
                    await walletService.UpdateRangeAsync(wallets);
                }
                _logger.LogInformation($"Task execution time: {sw.Elapsed}");
                await Task.Delay(TimeSpan.FromMinutes(taskDelay), stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BackgroundWorkerService stopped at: {DateTime.Now}");
            return base.StopAsync(cancellationToken);
        }
    }
}
