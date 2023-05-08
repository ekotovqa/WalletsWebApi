using AutoMapper;
using Core.Dto.ViewModels;
using Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WalletsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _service;
        private readonly IMapper _mapper;

        public WalletsController(IWalletService walletService, IMapper mapper)
        {
            _service = walletService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<WalletViewModel>>Get()
        {
            var wallets = await _service.GetAsync();
            return _mapper.Map<List<WalletViewModel>>(wallets.ToList());
        }
    }
}