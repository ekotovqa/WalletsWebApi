using Core.Models;
using Core.Services.Interface;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WalletsWebApi.Services.Interface;

namespace WalletsWebApi.Services
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IWeb3Service _web3Service;
        private readonly IOptions<AppSettings> _options;


        public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IServiceProvider serviceProvider, IWeb3Service web3Service, IOptions<AppSettings> options)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _web3Service = web3Service;
            _options = options;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var sw = Stopwatch.StartNew();
                IEnumerable<Wallet> wallets;
                List<TmpBalance> tmpBalances = new List<TmpBalance>();
                _logger.LogInformation($"BackgroundWorkerService started at: {DateTime.Now}");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var walletService = scope.ServiceProvider.GetRequiredService<IWalletService>();
                    wallets = await walletService.GetAsync();
                    int counter = 0;
                    foreach (var wallet in wallets)
                    {
                        if (wallet.TmpBalance == null)
                        {
                            var tmpBalance = new TmpBalance()
                            {
                                Balance = null,
                                UpdatedAt = DateTime.UtcNow,
                                WalletId = wallet.Id
                            };
                            tmpBalances.Add(tmpBalance);
                        }
                    }
                    if (tmpBalances.Count > 0)
                        await walletService.AddTmpBalancesRangeAsync(tmpBalances);
                    foreach (var wallet in wallets)
                    {
                        if (wallet.Address != null)
                        {
                            var balance = await _web3Service.GetBalance(wallet.Address);
                            var tmpBalance = new TmpBalance()
                            {
                                Id = wallet.TmpBalance.Id,
                                Balance = balance,
                                UpdatedAt = DateTime.UtcNow,
                                WalletId = wallet.Id
                            };
                            wallet.TmpBalance = tmpBalance;
                        }
                        counter++;
                        if (counter == 100)
                        {
                            await walletService.UpdateWalletsRangeAsync(wallets);
                            counter = 0;
                        }
                    }
                    await walletService.UpdateWalletsRangeAsync(wallets);
                }
                _logger.LogInformation($"Task execution time: {sw.Elapsed}");
                await Task.Delay(TimeSpan.FromMinutes(_options.Value.BackgroundWorkerServiceTaskDelay), stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BackgroundWorkerService stopped at: {DateTime.Now}");
            return base.StopAsync(cancellationToken);
        }
    }
}
