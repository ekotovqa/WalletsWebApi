using AutoMapper;
using Core.Dto.ViewModels;
using Core.Models;

namespace Core.Dto.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Wallet, WalletViewModel>().ForMember(walletViewModel => walletViewModel.Balance, expression => expression.MapFrom(wallet => wallet.TmpBalance.Balance))
                .ForMember(walletViewModel => walletViewModel.UpdatedAt, expression => expression.MapFrom(wallet => wallet.TmpBalance.UpdatedAt));
        }
    }
}
