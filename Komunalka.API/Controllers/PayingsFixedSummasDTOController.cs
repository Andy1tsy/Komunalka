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
    public class PayingsFixedSummasDTOController : ControllerBase
    {
        private readonly KomunalContext _context;
        private IMapper _mapper;

        public PayingsFixedSummasDTOController(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PayingsFixedSummas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayingFixedSummaDTO>>> GetPayingsFixedSummaDTO()
        {
            var payingsFixedSumma = await _context.PayingFixedSumma.ToListAsync();
            var payingsFixedSummaDTO = _mapper.Map<List<PayingFixedSummaDTO>>(payingsFixedSumma);
            return payingsFixedSummaDTO;
        }

        // GET: api/PayingsByCounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayingFixedSummaDTO>> GetPayingFixedSummaDTO(int id)
        {
            var payingFixedSumma = await _context.PayingFixedSumma.FindAsync(id);

            if (payingFixedSumma == null)
            {
                return NotFound();
            }

            var payingFixedSummaDTO = _mapper.Map<PayingFixedSummaDTO>(payingFixedSumma);

            return payingFixedSummaDTO;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayingFixedSummaDTO(int id, PayingFixedSummaDTO payingFixedSummaDTO)
        {
            var payingFixedSumma = _mapper.Map<PayingFixedSumma>(payingFixedSummaDTO);
            if (id != payingFixedSumma.Id)
            {
                return BadRequest();
            }

            _context.Entry(payingFixedSumma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayingFixedSummaExists(id))
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
        public async Task<ActionResult<PayingFixedSummaDTO>> PostPayingFixedSummaDTO(PayingFixedSummaDTO payingFixedSummaDTO)
        {
            var payingFixedSumma = _mapper.Map<PayingFixedSumma>(payingFixedSummaDTO);
            _context.PayingFixedSumma.Add(payingFixedSumma);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PayingFixedSummaExists(payingFixedSumma.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPayingFixedSummaDTO", new { id = payingFixedSumma.Id }, payingFixedSummaDTO);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PayingFixedSummaDTO>> DeletePayingFixedSumma(int id)
        {
            var payingFixedSumma = await _context.PayingFixedSumma.FindAsync(id);
            if (payingFixedSumma == null)
            {
                return NotFound();
            }

            _context.PayingFixedSumma.Remove(payingFixedSumma);
            await _context.SaveChangesAsync();

            var payingFixedSummaDTO = _mapper.Map<PayingFixedSummaDTO>(payingFixedSumma);
            return payingFixedSummaDTO;
        }

        private bool PayingFixedSummaExists(int id)
        {
            return _context.PayingFixedSumma.Any(e => e.Id == id);
        }
    }
}
