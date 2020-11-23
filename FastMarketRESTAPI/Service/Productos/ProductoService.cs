using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Productos
{
    public interface ProductoService
    {
        Task<DataCollection<ProductoSimpleDto>> GetAll(int page, int take);
        Task<DataCollection<ProductoUsuarioDto>> GetAllUsuario(int page, int take);
        Task<ProductoDto> GetById(int id);
        Task Create(ProductoCreateOrUpdateDto model);
        Task Update(int id, ProductoCreateOrUpdateDto model);
        Task Remove(int id);
    }
}
