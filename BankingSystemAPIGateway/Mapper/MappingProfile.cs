using AutoMapper;
using BankingSystemAPIGateway.Models;
using DataAccess.Entities;

namespace BankingSystemAPIGateway.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, User>().ReverseMap();
        }
    }
}
