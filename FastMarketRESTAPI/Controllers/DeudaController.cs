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
    [Route("deuda")]
    public class DeudaController : ControllerBase
    {
        private readonly DeudaService _service;
        public DeudaController(DeudaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataCollection<DeudaSimpleDto>>> GetAll(int page, int take)
        {
            return await _service.GetAll(page, take);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DeudaDto>> GetById(int id)
        {
            var aux = await _service.GetById(id);
            aux.Pagos = await _service.GetPagosById(id);
            return aux;
        }
        /// <summary>
        /// Solo el administrador puede actualizar la deuda
        /// </summary>
        /// <param name="id">id de la deuda</param>
        /// <param name="model">datos a actualizar de la deuda</param>
        /// <returns></returns>
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DeudaUpdateDto model)
        {
            await _service.Update(id, model);
            return Ok();
        }

    }
}
