/// Author: Brian Haynes
using AutoMapper;
using TransactionApp.Application;
using TransactionApp.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, TransactionDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
