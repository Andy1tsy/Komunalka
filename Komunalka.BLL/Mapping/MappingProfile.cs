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

            CreateMap<PayingComponent, PayingComponentDTO>().
                      ForMember(dto => dto.ServiceProviderName, cfg => cfg.MapFrom(sr => sr.ServiceProvider.Name)).
                      ForMember(dto => dto.ServiceType, cfg => cfg.MapFrom(sr => sr.ServiceProvider.ServiceType));
            CreateMap<PayingComponentDTO, PayingComponent>();

            CreateMap<List<PayingComponent>, List<PayingComponentDTO>>();
            CreateMap<List<PayingComponentDTO>, List<PayingComponent>>();



            CreateMap<ServiceProvider, ServiceProviderDTO>();
            CreateMap<ServiceProviderDTO, ServiceProvider>();

            CreateMap<List<ServiceProvider>, List<ServiceProviderDTO>>();
            CreateMap<List<ServiceProviderDTO>, List<ServiceProvider>>();
        }
    }
}
