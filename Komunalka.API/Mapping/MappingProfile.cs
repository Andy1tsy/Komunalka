using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Komunalka.API.DTO;
using Komunalka.DAL.Models;

namespace Komunalka.API.Mapping
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

            CreateMap<PayingByCounter, PayingByCounterDTO>();
            CreateMap<PayingByCounterDTO, PayingByCounter>();

            CreateMap<List<PayingByCounter>, List<PayingByCounterDTO>>();
            CreateMap<List<PayingByCounterDTO>, List<PayingByCounter>>();

            CreateMap<PayingFixedSumma, PayingFixedSummaDTO>();
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
