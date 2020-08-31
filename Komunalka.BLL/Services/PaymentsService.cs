using AutoMapper;
using Komunalka.BLL.Absract;
using Komunalka.BLL.DTO;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komunalka.BLL.Services
{
    public class PaymentsService : IPaymentsService
    {
        private KomunalContext _context;
        private IMapper _mapper;

        public PaymentsService(KomunalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<PaymentDTO> GetPaymentsDTO(int customerId)
        {

                var payments =  _context.Payment.Where(p => p.CustomerId == customerId)
                                        .ToListAsync();
                var paymentsDTO = _mapper.Map<List<PaymentDTO>>(payments);
                return paymentsDTO;
            
        }

        public PaymentDTO GetPaymentDTO(int id)
        {
            var payment = _context.Payment
                                   .Include(p => p.PayingComponent.ToList())
                                   .ThenInclude(p => p.ServiceProvider)
                                   .Where(c => c.Id == id).FirstOrDefaultAsync();

            var paymentDTO = _mapper.Map<PaymentDTO>(payment);
            return paymentDTO;
        }

        public void PutPaymentDTO(int id, PaymentDTO paymentDTO)
        {
            var payment = _mapper.Map<Payment>(paymentDTO);
            _context.Entry(payment).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public void PostPaymentDTO(PaymentDTO paymentDTO)
        {
            var payment = _mapper.Map<Payment>(paymentDTO);
            _context.Payment.Add(payment);
            _context.SaveChangesAsync();

        }

        public PaymentDTO DeletePaymentDTO(int id)
        {
            var payment = _context.Payment.Find(id);
            if (payment == null)
            {
                return null;
            }

            _context.Payment.Remove(payment);
            _context.SaveChangesAsync();
            var paymentDTO = _mapper.Map<PaymentDTO>(payment);
            return paymentDTO;
        }

        public bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.Id == id);
        }
    }
}
