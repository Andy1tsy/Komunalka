using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayingsByCountersController : ControllerBase
    {
        private readonly KomunalContext _context;

        public PayingsByCountersController(KomunalContext context)
        {
            _context = context;
        }

        // GET: api/PayingsByCounters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayingByCounter>>> GetPayingByCounter()
        {
            return await _context.PayingByCounter.ToListAsync();
        }

        // GET: api/PayingsByCounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayingByCounter>> GetPayingByCounter(int id)
        {
            var payingByCounter = await _context.PayingByCounter.FindAsync(id);

            if (payingByCounter == null)
            {
                return NotFound();
            }

            return payingByCounter;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayingByCounter(int id, PayingByCounter payingByCounter)
        {
            if (id != payingByCounter.Id)
            {
                return BadRequest();
            }

            _context.Entry(payingByCounter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<PayingByCounter>> PostPayingByCounter(PayingByCounter payingByCounter)
        {
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

            return CreatedAtAction("GetPayingByCounter", new { id = payingByCounter.Id }, payingByCounter);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PayingByCounter>> DeletePayingByCounter(int id)
        {
            var payingByCounter = await _context.PayingByCounter.FindAsync(id);
            if (payingByCounter == null)
            {
                return NotFound();
            }

            _context.PayingByCounter.Remove(payingByCounter);
            await _context.SaveChangesAsync();

            return payingByCounter;
        }

        private bool PayingByCounterExists(int id)
        {
            return _context.PayingByCounter.Any(e => e.Id == id);
        }
    }
}
