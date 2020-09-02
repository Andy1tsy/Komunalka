using Komunalka.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Komunalka.BLL.Abstract
{
    public interface ICustomersService
    {
        bool CustomerExists(int id);
        Task<CustomerDTO> DeleteCustomerDTO(int id);
        Task<CustomerDTO> GetCustomerDTO(int id);
        IAsyncEnumerable<CustomerDTO> GetCustomersDTO();
        void PostCustomerDTO(CustomerDTO customerDTO);
        void PutCustomerDTO(int id, CustomerDTO customerDTO);
    }
}