using AccountService.Domain.Entities;
using AccountService.Presentation.ViewModels;
using AutoMapper;

namespace AccountService.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponseViewModel>().ReverseMap();

            CreateMap<Account, AccountRequestViewModel>().ReverseMap();
        }

    }
}
