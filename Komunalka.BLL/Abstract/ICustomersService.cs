using Komunalka.BLL.DTO;
using System.Collections.Generic;

namespace Komunalka.BLL.Absract
{
    public interface ICustomersService
    {
        bool CustomerExists(int id);
        CustomerDTO DeleteCustomerDTO(int id);
        CustomerDTO GetCustomerDTO(int id);
        IEnumerable<CustomerDTO> GetCustomersDTO();
        void PostCustomerDTO(CustomerDTO customerDTO);
        void PutCustomerDTO(int id, CustomerDTO customerDTO);
    }
}