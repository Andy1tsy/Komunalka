using AutoMapper;
using Komunalka.BLL.DTO;
using Komunalka.BLL.Abstract;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunalka.BLL.Services
{
    public class PayingComponentsService : IPayingComponentsService
    {
        private KomunalContext _context;
        private IMapper _mapper;

        public PayingComponentsService(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public IAsyncEnumerable<PayingComponentDTO> GetPayingComponentsDTO(int paymentId)
        {
            var payingComponents = _context.PayingComponent
                                           .Where(c => c.PaymentId == paymentId)
                                           .ToListAsync();
            var payingComponentsDTO = _mapper.Map<List<PayingComponentDTO>>(payingComponents);
            return (IAsyncEnumerable<PayingComponentDTO>)payingComponentsDTO;
        }


        public async Task<PayingComponentDTO> GetPayingComponentDTO(int id)
        {
            var payingComponent = await _context.PayingComponent.FindAsync(id);

            if (payingComponent == null)
            {
                return null;
            }

            var payingComponentDTO = _mapper.Map<PayingComponentDTO>(payingComponent);

            return payingComponentDTO;
        }

        public async void PutPayingComponentDTO(int id, PayingComponentDTO payingComponentDTO)
        {
            var payingComponent = _mapper.Map<PayingComponent>(payingComponentDTO);
            _context.Entry(payingComponent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async void PostPayingComponentDTO(PayingComponentDTO payingComponentDTO)
        {
            var payingComponent = _mapper.Map<PayingComponent>(payingComponentDTO);
            await _context.PayingComponent.AddAsync(payingComponent);
            await _context.SaveChangesAsync();
        }


        public async Task<PayingComponentDTO> DeletePayingComponent(int id)
        {
            var payingComponent = await _context.PayingComponent.FindAsync(id);
            if (payingComponent == null)
            {
                return null;
            }

            _context.PayingComponent.Remove(payingComponent);
            await _context.SaveChangesAsync();

            var payingComponentDTO = _mapper.Map<PayingComponentDTO>(payingComponent);
            return payingComponentDTO;
        }

        public bool PayingComponentExists(int id)
        {
            return _context.PayingComponent.Any(e => e.Id == id);
        }
    }
}
