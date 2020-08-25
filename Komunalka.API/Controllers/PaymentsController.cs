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
using Komunalka.BLL.Services;
using Komunalka.API.DTO;

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private  KomunalContext _context;
        private IMapper _mapper;
        private PaymentsService _service;

        public PaymentsController(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _service = new PaymentsService(context, mapper);
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<IEnumerable<PaymentDTO>> GetPaymentsDTO(int customerId)
        {
            return await Task.Run(() => _service.GetPaymentsDTO(customerId));
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPaymentDTO(int customerId, int id)
        {
            var payment = await Task.Run(() => _service.GetPaymentDTO(customerId, id));

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDTO(int id, PaymentDTO paymentDTO)
        {
 

            try
            {
                await Task.Run(() => _service.PutPaymentDTO(id, paymentDTO));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPaymentDTO(PaymentDTO paymentDTO)
        {
            
            await Task.Run(() => _service.PostPaymentDTO(paymentDTO));

            return CreatedAtAction("GetPaymentDTO", new { id = paymentDTO.Id }, paymentDTO);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDTO>> DeletePayment( int id)
        {
 
           var paymentDTO = await Task.Run(() => _service.DeletePaymentDTO(id));

            return paymentDTO;
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.Id == id);
        }
    }
}
