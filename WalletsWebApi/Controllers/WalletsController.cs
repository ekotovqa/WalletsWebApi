using AutoMapper;
using Core.Dto.ViewModels;
using Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WalletsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> Get()
        {
            var wallets = await _service.GetAsync();
            return Ok(_mapper.Map<List<WalletViewModel>>(wallets));
        }
    }
}