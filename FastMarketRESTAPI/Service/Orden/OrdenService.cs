using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Orden
{
    public interface OrdenService
    {
        Task<DataCollection<OrdenSimpleDto>> GetAll(int page, int take);
        Task<OrdenDto> GetById(int id);
        Task Create(OrdenCreateOrUpdateDto model);
        Task Remove(int id);
    }
}
