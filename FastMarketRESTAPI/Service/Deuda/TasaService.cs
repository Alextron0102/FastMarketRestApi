using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Deuda
{
    public interface TasaService
    {
        Task<DataCollection<TasaDto>> GetAll(int page, int take);
        Task<DataCollection<TasaSimpleDto>> GetAllSimple(int page, int take);
        Task<TasaDto> GetById(int id);
        Task Create(TasaCreateOrUpdateDto model);
        Task Update(int id, TasaCreateOrUpdateDto model);
        Task Remove(int id);
    }
}
