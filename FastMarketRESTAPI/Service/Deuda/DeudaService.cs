using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Deuda
{
    public interface DeudaService
    {
        //La tasa no se crea ni se destruye, sólo se transforma
        Task<DataCollection<DeudaSimpleDto>> GetAll(int page, int take);
        Task<DeudaDto> GetById(int id);
        Task<List<PagoDto>> GetPagosById(int id);
        Task Update(int id, DeudaUpdateDto model);
    }
}
