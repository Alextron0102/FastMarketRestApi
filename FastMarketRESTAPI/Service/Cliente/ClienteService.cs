using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Cliente
{
    public interface ClienteService
    {
        Task<DataCollection<ClienteDto>> GetAll(int page, int take);
        Task<DataCollection<ClienteSimpleDto>> GetAllSimple(int page, int take);
        Task<ClienteDto> GetById(int id);
        Task Create(ClienteCreateOrUpdate model);
        Task Update(int id, ClienteCreateOrUpdate model);
        Task Remove(int id);
    }
}
