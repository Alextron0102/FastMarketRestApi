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
    [Route("categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;
        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataCollection<CategoriaSimpleDto>>> GetAll(int page, int take)
        {
            return await _service.GetAll(page, take);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDto>> GetById(int id)
        {
            return await _service.GetById(id);
        }
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpPost]
        public async Task<ActionResult> Create(CategoriaCreateOrUpdateDto model)
        {
            await _service.Create(model);
            return Ok();
        }
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoriaCreateOrUpdateDto model)
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
