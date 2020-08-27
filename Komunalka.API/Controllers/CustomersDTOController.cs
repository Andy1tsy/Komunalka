using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Komunalka.BLL.DTO;
using Komunalka.BLL.Services;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Windows;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
        public class CustomersDTOController : ControllerBase
        {
            private  KomunalContext _context;
            private IMapper _mapper;
            private CustomersService _service;

            public CustomersDTOController(KomunalContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
                _service = new CustomersService(_context, _mapper);
            }

            // GET: api/Customers
            [HttpGet]
            public async Task<IEnumerable<CustomerDTO>> GetCustomersDTO()
            {
 
                var customersDTO = await Task.Run(() =>_service.GetCustomersDTO());
                return  customersDTO;
            }

            // GET: api/Customers/5
            [HttpGet("{id}")]
            public async Task<ActionResult<CustomerDTO>> GetCustomerDTO(int id)
            {

                var customerDTO = await Task.Run(() =>_service.GetCustomerDTO(id));
                if (customerDTO == null)
                {
                    return NotFound();
                }
                return customerDTO;
            }

            // PUT: api/Customers/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            [HttpPut("{id}")]
            public async Task<IActionResult> PutCustomerDTO(int id, CustomerDTO customerDTO)
            {
 

                try
                {
                    await Task.Run(() => _service.PutCustomerDTO(id, customerDTO));
                    return Ok();
                }
                catch (Exception)
                {
                    if (!_service.CustomerExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
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

                try
                {
                await Task.Run(() => _service.PostCustomerDTO(customerDTO));
                }
                catch (DbUpdateException)
                {
                    if (_service.CustomerExists(customerDTO.Id))
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
                var customerDTO = await Task.Run(() => _service.DeleteCustomerDTO(id));
                if (customerDTO == null)
                {
                    return NotFound();
                }
                return customerDTO;
            }

 
        }
}
