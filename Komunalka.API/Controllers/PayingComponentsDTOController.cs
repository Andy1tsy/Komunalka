using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Komunalka.BLL.Abstract;
using Komunalka.BLL.DTO;
using Komunalka.BLL.Services;
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
        private IPayingComponentsService _service;

        public PayingComponentsDTOController(KomunalContext context, IMapper mapper, IPayingComponentsService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        // GET: api/PayingsByCounters
        [HttpGet]
        //
        //  здесь непонятно, как получить ActionResult ? без него работает
        //
        public async Task<IAsyncEnumerable<PayingComponentDTO>> GetPayingComponentsDTO(int paymentId)
        {
            var payingComponentsDTO = await Task.Run( () => _service.GetPayingComponentsDTO(paymentId));
            return  payingComponentsDTO;
        }

        // GET: api/PayingsByCounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayingComponentDTO>> GetPayingComponentDTO(int id)
        {
            var payingComponentDTO = await Task.Run(() => _service.GetPayingComponentDTO(id));
            if (payingComponentDTO == null)
            {
                return NotFound();
            }
            return payingComponentDTO;
        }

        // PUT: api/PayingsByCounters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayingComponentDTO(int id, PayingComponentDTO payingComponentDTO)
        {

            try
            {
                await Task.Run(() =>_service.PutPayingComponentDTO(id, payingComponentDTO));
                return Ok();
            }
            catch (Exception)
            {
                if (id != payingComponentDTO.Id)
                {
                    return BadRequest();
                }
                if (!_service.PayingComponentExists(id))
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
        public async Task<ActionResult<PayingComponentDTO>> PostPayingComponentDTO(PayingComponentDTO payingComponentDTO)
        {
            try
            {
                await Task.Run(() => _service.PostPayingComponentDTO(payingComponentDTO));
            }
            catch (DbUpdateException)
            {
                if (_service.PayingComponentExists(payingComponentDTO.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPayingComponent", new { id = payingComponentDTO.Id }, payingComponentDTO);
        }

        // DELETE: api/PayingsByCounters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PayingComponentDTO>> DeletePayingComponent(int id)
        {

            var payingComponentDTO = await Task.Run(() => _service.DeletePayingComponent(id));
            if (payingComponentDTO == null)
            {
                return NotFound();
            }
            return payingComponentDTO;
        }

        //public bool PayingComponentExists(int id)
        //{
        //    return _context.PayingComponent.Any(e => e.Id == id);
        //}
    }
}
