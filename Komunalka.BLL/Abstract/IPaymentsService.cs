using Komunalka.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Komunalka.BLL.Abstract
{
    public interface IPaymentsService
    {
        Task<PaymentDTO> DeletePaymentDTO(int id);
        Task<PaymentDTO> GetPaymentDTO(int id);
        IAsyncEnumerable<PaymentDTO> GetPaymentsDTO(int customerId);
        bool PaymentExists(int id);
        void PostPaymentDTO(PaymentDTO paymentDTO);
        void PutPaymentDTO(int id, PaymentDTO paymentDTO);
    }
}