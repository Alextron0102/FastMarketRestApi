using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Model.Identity;
using FastMarketRESTAPI.Service.Orden;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = RoleHelper.Admin)]
    [Route("orden")]
    public class OrdenController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OrdenService _service;
        public OrdenController(OrdenService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<DataCollection<OrdenSimpleDto>>> GetAll(int page, int take)
        {
            return await _service.GetAll(page, take);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenDto>> GetById(int id)
        {
            return await _service.GetById(id);
        }
        [HttpPost]
        public async Task<ActionResult> Create(OrdenCreateOrUpdateDto model)
        {
            //Aqui tenemos que validar que sólo el administrador pueda poner diferente idCliente en el model
            try
            {
                await _service.Create(model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return Ok();
        }
    }
}
