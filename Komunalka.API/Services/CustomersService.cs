using AutoMapper;
using Komunalka.API.DTO;
using Komunalka.DAL.KomunalDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Komunalka.DAL.Models;

namespace Komunalka.BLL.Services
{
    public class CustomersService
    {
        private KomunalContext _context;
        private IMapper _mapper;

        public CustomersService( KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public  IEnumerable<CustomerDTO> GetCustomersDTO()
        {
            var customers =  _context.Customer
                               .Include(c => c.Payment)
                               .ThenInclude(p => p.PayingByCounter.ToList())
                               .ThenInclude(p => p.ServiceProvider)
                               .ToListAsync();
            var customersDTO = _mapper.Map<List<CustomerDTO>>(customers);
             return customersDTO;
        }

        public CustomerDTO GetCustomerDTO(int id)
        {
            var customer =  _context.Customer.Include(c => c.Payment)
                                       .ThenInclude(p => p.PayingByCounter)
                                       .ThenInclude(p => p.ServiceProvider)
                                       .Where(c => c.Id == id).FirstOrDefaultAsync();
 
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        public void PutCustomerDTO(int id, CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChangesAsync();

        }

        public void PostCustomerDTO( CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            _context.Customer.Add(customer);
             _context.SaveChangesAsync();

        }

        public CustomerDTO DeleteCustomerDTO(int id)
        {
            var customer =  _context.Customer.Find(id);
            if (customer == null)
            {
                return null;
            }

            _context.Customer.Remove(customer);
            _context.SaveChangesAsync();
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        public bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
