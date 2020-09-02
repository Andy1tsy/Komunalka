using Komunalka.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Komunalka.BLL.Abstract
{
    public interface IServiceProvidersService
    {
        Task<ServiceProviderDTO> DeleteServiceProvider(int id);
        Task<ServiceProviderDTO> GetServiceProviderDTO(int id);
        IAsyncEnumerable<ServiceProviderDTO> GetServiceProvidersDTO();
        void PostServiceProviderDTO(ServiceProviderDTO serviceProviderDTO);
        void PutServiceProviderDTO(int id, ServiceProviderDTO serviceProviderDTO);
        public bool ServiceProviderExists(int id);

    }
}