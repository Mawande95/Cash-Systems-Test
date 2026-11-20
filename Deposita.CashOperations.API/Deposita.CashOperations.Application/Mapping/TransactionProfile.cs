using AutoMapper;
using Deposita.CashOperations.Application.DTOs;
using Deposita.CashOperations.Domain.Entities;


namespace Deposita.CashOperations.Application.Mapping
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<UpdateProductDto, Transaction>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateProductDto, Transaction>();
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
