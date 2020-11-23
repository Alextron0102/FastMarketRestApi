using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Service.Deuda;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = RoleHelper.Admin)]
    [Route("tasa")]
    public class TasaController : ControllerBase
    {
        private readonly TasaService _service;
        public TasaController(TasaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataCollection<TasaDto>>> GetAll(int page, int take)
        {
            return await _service.GetAll(page, take);
        }
        [HttpGet("simple")]
        public async Task<ActionResult<DataCollection<TasaSimpleDto>>> GetAllSimple(int page, int take)
        {
            return await _service.GetAllSimple(page, take);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TasaDto>> GetById(int id)
        {
            return await _service.GetById(id);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TasaCreateOrUpdateDto model)
        {
            await _service.Create(model);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TasaCreateOrUpdateDto model)
        {
            await _service.Update(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return Ok();
        }
    }
}
