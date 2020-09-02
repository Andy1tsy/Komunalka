using Komunalka.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Komunalka.BLL.Abstract
{
    public interface IPayingComponentsService
    {
        Task<PayingComponentDTO> DeletePayingComponent(int id);
        Task<PayingComponentDTO> GetPayingComponentDTO(int id);
        IAsyncEnumerable<PayingComponentDTO> GetPayingComponentsDTO(int paymentId);
        void PostPayingComponentDTO(PayingComponentDTO PayingComponentDTO);
        void PutPayingComponentDTO(int id, PayingComponentDTO PayingComponentDTO);
        bool PayingComponentExists(int id);
    }
}