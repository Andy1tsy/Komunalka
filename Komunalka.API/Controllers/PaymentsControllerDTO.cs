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
using Komunalka.BLL.Services;

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsControllerDTO : ControllerBase
    {
        private  KomunalContext _context;
        private IMapper _mapper;
        private PaymentsService _service;

        public PaymentsControllerDTO(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _service = new PaymentsService(_context, _mapper);
        }

        // GET: api/PaymentsDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsDTO()
        {
            var payments = await _context.Payment.ToListAsync();
            var paymentsDTO = _mapper.Map<List<PaymentDTO>>(payments);
            return paymentsDTO;
        }

        // GET: api/PaymentsDTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPaymentDTO(int id)
        {
            var payment = await _context.Payment.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            var paymentDTO = _mapper.Map<PaymentDTO>(payment);

            return paymentDTO;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDTO(int id, PaymentDTO paymentDTO)
        {
            var payment = _mapper.Map<Payment>(paymentDTO);
            if (id != payment.Id)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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
        public async Task<ActionResult<PaymentDTO>> PostPaymentDTO(PaymentDTO paymentDTO)
        {
            var payment = _mapper.Map<Payment>(paymentDTO);
            _context.Payment.Add(payment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaymentExists(payment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPaymentDTO", new { id = payment.Id }, paymentDTO);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDTO>> DeletePaymentDTO(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();

            var paymentDTO = _mapper.Map<PaymentDTO>(payment);
            return paymentDTO;
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.Id == id);
        }
    }
}
