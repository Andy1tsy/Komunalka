using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Komunalka.API.DTO;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
        public class CustomersControllerDTO : ControllerBase
        {
            private readonly KomunalContext _context;
            private IMapper _mapper;

            public CustomersControllerDTO(KomunalContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            // GET: api/Customers
            [HttpGet]
            public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomersDTO()
            {
                var customers =  await _context.Customer
                                               .Include(c => c.Payment)
                                               .ThenInclude(p => p.PayingByCounter.ToList())
                                               .ThenInclude(p => p.ServiceProvider)
                                               .ToListAsync();
                var cuatomersDTO = _mapper.Map<List<CustomerDTO>>(customers);
                return cuatomersDTO;
            }

            // GET: api/Customers/5
            [HttpGet("{id}")]
            public async Task<ActionResult<CustomerDTO>> GetCustomerDTO(int id)
            {
            var customer = await _context.Customer.Include(c => c.Payment)
                                                  .ThenInclude(p => p.PayingByCounter)
                                                  .ThenInclude(p => p.ServiceProvider)
                                                  .Where(c => c.Id == id).FirstOrDefaultAsync();

                if (customer == null)
                {
                    return NotFound();
                }

                var customerDTO = _mapper.Map<CustomerDTO>(customer);
                return customerDTO;
            }

            // PUT: api/Customers/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            [HttpPut("{id}")]
            public async Task<IActionResult> PutCustomer(int id, CustomerDTO customerDTO)
            {
            var customer = _mapper.Map<Customer>(customerDTO);
                if (id != customer.Id)
                {
                    return BadRequest();
                }

                _context.Entry(customer).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            return NoContent();
            }

            // POST: api/Customers
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            [HttpPost]
            public async Task<ActionResult<CustomerDTO>> PostCustomerDTO(CustomerDTO customerDTO)
            {
            var customer = _mapper.Map<Customer>(customerDTO);
                _context.Customer.Add(customer);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (CustomerExists(customer.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetCustomerDTO", new { id = customerDTO.Id }, customerDTO);
            // ? Didn`t understand
            }

            // DELETE: api/Customers/5
            [HttpDelete("{id}")]
            public async Task<ActionResult<CustomerDTO>> DeleteCustomerDTO(int id)
            {
                var customer = await _context.Customer.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
            var customerDTO = _mapper.Map<CustomerDTO>(customer);

                return customerDTO;
            }

            private bool CustomerExists(int id)
            {
                return _context.Customer.Any(e => e.Id == id);
            }
        }
}
