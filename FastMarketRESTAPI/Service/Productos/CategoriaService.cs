using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Productos
{
    public interface CategoriaService
    {
        Task<DataCollection<CategoriaSimpleDto>> GetAll(int page, int take);
        Task<CategoriaDto> GetById(int id);
        Task Create(CategoriaCreateOrUpdateDto model);
        Task Update(int id, CategoriaCreateOrUpdateDto model);
        Task Remove(int id);
    }
}
