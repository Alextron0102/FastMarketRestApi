using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Service.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Controllers
{
    [ApiController]
    [Route("productos")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _service;
        public ProductoController(ProductoService service)
        {
            _service = service;
        }
        /// <summary>
        /// La vista de los productos para el administrador
        /// </summary>
        /// <param name="page">página de la cual se sacan los valores</param>
        /// <param name="take">Tamaño de la página de los resultados</param>
        /// <returns></returns>
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpGet("admin")]
        public async Task<ActionResult<DataCollection<ProductoSimpleDto>>> GetAll(int page, int take)
        {
            return await _service.GetAll(page, take);
        }
        /// <summary>
        /// La vista de los productos para el usuario
        /// </summary>
        /// <param name="page">página de la cual se sacan los valores</param>
        /// <param name="take">Tamaño de la página de los resultados</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<DataCollection<ProductoUsuarioDto>>> GetAllUsuario(int page, int take)
        {
            return await _service.GetAllUsuario(page, take);
        }
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetById(int id)
        {
            return await _service.GetById(id);
        }
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpPost]
        public async Task<ActionResult> Create(ProductoCreateOrUpdateDto model)
        {
            await _service.Create(model);
            return Ok();
        }
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductoCreateOrUpdateDto model)
        {
            await _service.Update(id,model);
            return Ok();
        }
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return Ok();
        }
    }
}
