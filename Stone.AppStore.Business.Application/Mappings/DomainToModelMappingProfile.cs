using AutoMapper;
using Stone.AppStore.Business.Application.Models;
using Stone.AppStore.Business.Domain;
using Stone.AppStore.Business.Domain.Entities;

namespace Stone.AppStore.Business.Application.Mappings
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<CreditCard, CreditCardModel>().ReverseMap();
        }
    }
}
