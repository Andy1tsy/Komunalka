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
    public class PayingsByCountersDTOController : ControllerBase
    {
        private readonly KomunalContext _context;
        private IMapper _mapper;

        public PayingsByCountersDTOController(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PayingsByCounters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayingByCounterDTO>>> GetPayingsByCounterDTO()
        {
            var payingsByCounter = await _context.PayingByCounter.ToListAsync();
            var payingsByCounterDTO = _mapper.Map<List<PayingByCounterDTO>>(payingsByCounter);
            return payingsByCounterDTO;
        }

        // GET: api/PayingsByCounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayingByCounterDTO>> GetPayingByCounterDTO(int id)
        {
            var payingByCounter = await _context.PayingByCounter.FindAsync(id);

            if (payingByCounter == null)
            {
                return NotFound();
            }

            var payingByCounterDTO = _mapper.Map<PayingByCounterDTO>(payingByCounter);

            return payingByCounterDTO;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayingByCounterDTO(int id, PayingByCounterDTO payingByCounterDTO)
        {
            var payingByCounter = _mapper.Map<PayingByCounter>(payingByCounterDTO);
            if (id != payingByCounter.Id)
            {
                return BadRequest();
            }

            _context.Entry(payingByCounter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayingByCounterExists(id))
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
        public async Task<ActionResult<PayingByCounterDTO>> PostPayingByCounterDTO(PayingByCounterDTO payingByCounterDTO)
        {
            var payingByCounter = _mapper.Map<PayingByCounter>(payingByCounterDTO);
            _context.PayingByCounter.Add(payingByCounter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PayingByCounterExists(payingByCounter.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPayingByCounter", new { id = payingByCounter.Id }, payingByCounterDTO);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PayingByCounterDTO>> DeletePayingByCounter(int id)
        {
            var payingByCounter = await _context.PayingByCounter.FindAsync(id);
            if (payingByCounter == null)
            {
                return NotFound();
            }

            _context.PayingByCounter.Remove(payingByCounter);
            await _context.SaveChangesAsync();

            var payingByCounterDTO = _mapper.Map<PayingByCounterDTO>(payingByCounter);
            return payingByCounterDTO;
        }

        private bool PayingByCounterExists(int id)
        {
            return _context.PayingByCounter.Any(e => e.Id == id);
        }
    }
}
