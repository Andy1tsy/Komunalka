using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.BLL.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public  List<PaymentDTO> PaymentsDTO { get; set; }
        public CustomerDTO()
        {
            PaymentsDTO = new List<PaymentDTO>();
        }
    }
}
