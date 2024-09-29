using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Application.Commands;
using Application.Queries;
namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<CreateTransactionDto, CreateTransactionCommand>().ReverseMap();
            CreateMap<CancelTransactionDto, CancelTransactionCommand>().ReverseMap();
            CreateMap<RefundTransactionDto, RefundTransactionCommand>().ReverseMap();
            CreateMap<ReportQueryDto, ReportQuery>().ReverseMap();
        }
    }
}