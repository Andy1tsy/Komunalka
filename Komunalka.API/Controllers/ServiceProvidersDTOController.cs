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
using Komunalka.API.DTO;

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidersDTOController : ControllerBase
    {
        private readonly KomunalContext _context;
        private IMapper _mapper;

        public ServiceProvidersDTOController(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ServiceProvidersDTO
        [HttpGet]
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
            var ServiceProvider = await _context.ServiceProvider.FindAsync(id);

            if (ServiceProvider == null)
            {
                return NotFound();
            }

            var ServiceProviderDTO = _mapper.Map<ServiceProviderDTO>(ServiceProvider);

            return ServiceProviderDTO;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceProviderDTO(int id, ServiceProviderDTO ServiceProviderDTO)
        {
            var ServiceProvider = _mapper.Map<ServiceProvider>(ServiceProviderDTO);
            if (id != ServiceProvider.Id)
            {
                return BadRequest();
            }

            _context.Entry(ServiceProvider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceProviderExists(id))
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
        public async Task<ActionResult<ServiceProviderDTO>> PostServiceProviderDTO(ServiceProviderDTO ServiceProviderDTO)
        {
            var ServiceProvider = _mapper.Map<ServiceProvider>(ServiceProviderDTO);
            _context.ServiceProvider.Add(ServiceProvider);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServiceProviderExists(ServiceProvider.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetServiceProviderDTO", new { id = ServiceProvider.Id }, ServiceProviderDTO);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceProviderDTO>> DeleteServiceProviderDTO(int id)
        {
            var ServiceProvider = await _context.ServiceProvider.FindAsync(id);
            if (ServiceProvider == null)
            {
                return NotFound();
            }

            _context.ServiceProvider.Remove(ServiceProvider);
            await _context.SaveChangesAsync();

            var ServiceProviderDTO = _mapper.Map<ServiceProviderDTO>(ServiceProvider);
            return ServiceProviderDTO;
        }

        private bool ServiceProviderExists(int id)
        {
            return _context.ServiceProvider.Any(e => e.Id == id);
        }
    }
}
