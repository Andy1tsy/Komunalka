using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Komunalka.BLL.DTO;
using Komunalka.DAL.Models;

namespace Komunalka.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();

            CreateMap<List<Customer>, List<CustomerDTO>>();
            CreateMap<List<CustomerDTO>, List<Customer>>();

            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();

            CreateMap<List<Payment>, List<PaymentDTO>>();
            CreateMap<List<PaymentDTO>, List<Payment>>();

            CreateMap<PayingByCounter, PayingByCounterDTO>().
                      ForMember(dto => dto.ServiceProviderName, cfg => cfg.MapFrom(sr => sr.ServiceProvider.Name));
            CreateMap<PayingByCounterDTO, PayingByCounter>();

            CreateMap<List<PayingByCounter>, List<PayingByCounterDTO>>();
            CreateMap<List<PayingByCounterDTO>, List<PayingByCounter>>();

            CreateMap<PayingFixedSumma, PayingFixedSummaDTO>().
                      ForMember( dto => dto.ServiceProviderName, cfg => cfg.MapFrom(sr => sr.ServiceProvider.Name) );
            CreateMap<PayingFixedSummaDTO, PayingFixedSumma>();

            CreateMap<List<PayingFixedSumma>, List<PayingFixedSummaDTO>>();
            CreateMap<List<PayingFixedSummaDTO>, List<PayingFixedSumma>>();

            CreateMap<ServiceProvider, ServiceProviderDTO>();
            CreateMap<ServiceProviderDTO, ServiceProvider>();

            CreateMap<List<ServiceProvider>, List<ServiceProviderDTO>>();
            CreateMap<List<ServiceProviderDTO>, List<ServiceProvider>>();
        }
    }
}
