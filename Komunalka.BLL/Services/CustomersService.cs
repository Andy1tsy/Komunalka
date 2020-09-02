using AutoMapper;
using Komunalka.BLL.DTO;
using Komunalka.DAL.KomunalDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Komunalka.DAL.Models;
using System.Threading.Tasks;
using Komunalka.BLL.Abstract;

namespace Komunalka.BLL.Services
{
    public class CustomersService : ICustomersService
    {
        private KomunalContext _context;
        private IMapper _mapper;

        public CustomersService(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IAsyncEnumerable<CustomerDTO> GetCustomersDTO()
        {
            var customers = _context.Customer
                                    .ToListAsync();
            var customersDTO = _mapper.Map<List<CustomerDTO>>(customers);
            return (IAsyncEnumerable<CustomerDTO>)customersDTO;
        }

        public async Task<CustomerDTO> GetCustomerDTO(int id)
        {
            var customer = await _context.Customer.Include(c => c.Payment)
                                   .Where(c => c.Id == id).FirstOrDefaultAsync();

            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        public async void PutCustomerDTO(int id, CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async void PostCustomerDTO(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

        }

        public async Task<CustomerDTO> DeleteCustomerDTO(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return null;
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        public bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
