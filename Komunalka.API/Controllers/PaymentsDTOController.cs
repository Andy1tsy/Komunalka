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
using Komunalka.BLL.Services;
using Komunalka.BLL.Abstract;

namespace Komunalka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsDTOController : ControllerBase
    {
        private  KomunalContext _context;
        private IMapper _mapper;
        private IPaymentsService _service;

        public PaymentsDTOController(KomunalContext context, IMapper mapper, IPaymentsService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        // GET: api/PaymentsDTO
        [HttpGet]
        //
        //  здесь непонятно, как получить ActionResult ? без него работает
        //
        public async Task<IAsyncEnumerable<PaymentDTO>> GetPaymentsDTO(int customerId)
        {
            var paymentsDTO = await Task.Run(() => _service.GetPaymentsDTO(customerId));
            return  paymentsDTO;
        }

        // GET: api/PaymentsDTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPaymentDTO(int id)
        {
            var paymentDTO = await Task.Run(() => _service.GetPaymentDTO(id));

            if (paymentDTO == null)
            {
                return NotFound();
            }
            return paymentDTO;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDTO(int id, PaymentDTO paymentDTO)
        {
            try
            {
                await Task.Run(() => _service.PutPaymentDTO(id, paymentDTO));
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (id != paymentDTO.Id)
                {
                    return BadRequest();
                }
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

        // POST: api/PayingsByCounters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> PostPaymentDTO(PaymentDTO paymentDTO)
        {

            try
            {
                await Task.Run(() => _service.PostPaymentDTO( paymentDTO));
            }
            catch (DbUpdateException)
            {
                if (_service.PaymentExists(paymentDTO.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPaymentDTO", new { id = paymentDTO.Id }, paymentDTO);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDTO>> DeletePaymentDTO(int id)
        {
            var paymentDTO = await Task.Run(() => _service.DeletePaymentDTO(id));
            if (paymentDTO == null)
            {
                return NotFound();
            }
  
            return paymentDTO;
        }

        //private bool PaymentExists(int id)
        //{
        //    return _context.Payment.Any(e => e.Id == id);
        //}
    }
}
