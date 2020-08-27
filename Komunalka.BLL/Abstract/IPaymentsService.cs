using Komunalka.BLL.DTO;
using System.Collections.Generic;

namespace Komunalka.BLL.Absract
{
    public interface IPaymentsService
    {
        PaymentDTO DeletePaymentDTO(int id);
        PaymentDTO GetPaymentDTO(int customerId, int id);
        IEnumerable<PaymentDTO> GetPaymentsDTO(int customerId);
        bool PaymentExists(int id);
        void PostPaymentDTO(PaymentDTO paymentDTO);
        void PutPaymentDTO(int id, PaymentDTO paymentDTO);
    }
}