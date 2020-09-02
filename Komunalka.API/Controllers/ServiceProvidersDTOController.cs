using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;
using AutoMapper;
using Komunalka.BLL.DTO;
using Komunalka.BLL.Abstract;

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidersDTOController : ControllerBase
    {
        private readonly KomunalContext _context;
        private IMapper _mapper;
        private IServiceProvidersService _service;

        public ServiceProvidersDTOController(KomunalContext context, IMapper mapper, IServiceProvidersService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        // GET: api/ServiceProvidersDTO
        [HttpGet]
        //
        //  здесь непонятно, как получить ActionResult ? без него работает
        //
        public async Task<ActionResult<IEnumerable<ServiceProviderDTO>>> GetServiceProvidersDTO()
        {
            var ServiceProviders = await _context.ServiceProvider.ToListAsync();
            var ServiceProvidersDTO = _mapper.Map<List<ServiceProviderDTO>>(ServiceProviders);
            return ServiceProvidersDTO;
        }

        // GET: api/ServiceProvidersDTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceProviderDTO>> GetServiceProviderDTO(int id)
        {

            var serviceProviderDTO = await Task.Run(() => _service.GetServiceProviderDTO(id));
            if (serviceProviderDTO == null)
            {
                return NotFound();
            }
            return serviceProviderDTO;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceProviderDTO(int id, ServiceProviderDTO serviceProviderDTO)
        {


            try
            {
                await Task.Run(() => _service.PutServiceProviderDTO(id, serviceProviderDTO));
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (id != serviceProviderDTO.Id)
                {
                    return BadRequest();
                }
                if (!_service.ServiceProviderExists(id))
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

        // POST: api/PayingsByCounters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ServiceProviderDTO>> PostServiceProviderDTO(ServiceProviderDTO serviceProviderDTO)
        {
     
            try
            {
                await Task.Run(() => _service.PostServiceProviderDTO(serviceProviderDTO));
            }
            catch (DbUpdateException)
            {
                if (_service.ServiceProviderExists(serviceProviderDTO.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetServiceProviderDTO", new { id = serviceProviderDTO.Id }, serviceProviderDTO);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceProviderDTO>> DeleteServiceProviderDTO(int id)
        {
 
            var serviceProviderDTO = await Task.Run(() => _service.DeleteServiceProvider(id));
            if (serviceProviderDTO == null)
            {
                return NotFound();
            }
            return serviceProviderDTO;
        }

        //private bool ServiceProviderExists(int id)
        //{
        //    return _context.ServiceProvider.Any(e => e.Id == id);
        //}
    }
}
