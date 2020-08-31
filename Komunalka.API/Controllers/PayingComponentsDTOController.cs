using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Komunalka.BLL.DTO;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayingComponentsDTOController : ControllerBase
    {
        private readonly KomunalContext _context;
        private IMapper _mapper;

        public PayingComponentsDTOController(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PayingsByCounters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayingComponentDTO>>> GetPayingComponentsDTO(int paymentId)
        {
            var payingComponents = await _context.PayingComponent.Where(c => c.PaymentId == paymentId).ToListAsync();
            var payingComponentsDTO = _mapper.Map<List<PayingComponentDTO>>(payingComponents);
            return payingComponentsDTO;
        }

        // GET: api/PayingsByCounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayingComponentDTO>> GetPayingComponentDTO(int id)
        {
            var payingComponent = await _context.PayingComponent.FindAsync(id);

            if (payingComponent == null)
            {
                return NotFound();
            }

            var payingComponentDTO = _mapper.Map<PayingComponentDTO>(payingComponent);

            return payingComponentDTO;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayingComponentDTO(int id, PayingComponentDTO PayingComponentDTO)
        {
            var PayingComponent = _mapper.Map<PayingComponent>(PayingComponentDTO);
            if (id != PayingComponent.Id)
            {
                return BadRequest();
            }

            _context.Entry(PayingComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayingComponentExists(id))
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
        public async Task<ActionResult<PayingComponentDTO>> PostPayingComponentDTO(PayingComponentDTO PayingComponentDTO)
        {
            var payingComponent = _mapper.Map<PayingComponent>(PayingComponentDTO);
            _context.PayingComponent.Add(payingComponent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PayingComponentExists(payingComponent.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPayingComponent", new { id = payingComponent.Id }, PayingComponentDTO);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PayingComponentDTO>> DeletePayingComponent(int id)
        {
            var PayingComponent = await _context.PayingComponent.FindAsync(id);
            if (PayingComponent == null)
            {
                return NotFound();
            }

            _context.PayingComponent.Remove(PayingComponent);
            await _context.SaveChangesAsync();

            var PayingComponentDTO = _mapper.Map<PayingComponentDTO>(PayingComponent);
            return PayingComponentDTO;
        }

        private bool PayingComponentExists(int id)
        {
            return _context.PayingComponent.Any(e => e.Id == id);
        }
    }
}
