using AutoMapper;
using Komunalka.BLL.Abstract;
using Komunalka.BLL.DTO;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunalka.BLL.Services
{
    public class ServiceProvidersService : IServiceProvidersService
    {
        private KomunalContext _context;
        private IMapper _mapper;

        public ServiceProvidersService(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public IAsyncEnumerable<ServiceProviderDTO> GetServiceProvidersDTO()
        {
            var serviceProviders = _context.ServiceProvider
                                           .ToListAsync();
            var serviceProvidersDTO = _mapper.Map<List<ServiceProviderDTO>>(serviceProviders);
            return (IAsyncEnumerable<ServiceProviderDTO>)serviceProvidersDTO;
        }


        public async Task<ServiceProviderDTO> GetServiceProviderDTO(int id)
        {
            var serviceProvider = await _context.ServiceProvider.FindAsync(id);

            if (serviceProvider == null)
            {
                return null;
            }

            var serviceProviderDTO = _mapper.Map<ServiceProviderDTO>(serviceProvider);

            return serviceProviderDTO;
        }

        public async void PutServiceProviderDTO(int id, ServiceProviderDTO serviceProviderDTO)
        {
            var serviceProvider = _mapper.Map<ServiceProvider>(serviceProviderDTO);
            _context.Entry(serviceProvider).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async void PostServiceProviderDTO(ServiceProviderDTO serviceProviderDTO)
        {
            var serviceProvider = _mapper.Map<ServiceProvider>(serviceProviderDTO);
            await _context.ServiceProvider.AddAsync(serviceProvider);
            await _context.SaveChangesAsync();
        }


        public async Task<ServiceProviderDTO> DeleteServiceProvider(int id)
        {
            var serviceProvider = await _context.ServiceProvider.FindAsync(id);
            if (serviceProvider == null)
            {
                return null;
            }

            _context.ServiceProvider.Remove(serviceProvider);
            await _context.SaveChangesAsync();

            var serviceProviderDTO = _mapper.Map<ServiceProviderDTO>(serviceProvider);
            return serviceProviderDTO;
        }

        public bool ServiceProviderExists(int id)
        {
            return _context.ServiceProvider.Any(e => e.Id == id);
        }
    }
}
