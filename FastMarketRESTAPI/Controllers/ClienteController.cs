using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Model.Identity;
using FastMarketRESTAPI.Service.Cliente;
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
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ClienteService _service;
        public ClienteController(ClienteService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }        
        [HttpGet]
        public async Task<ActionResult<DataCollection<ClienteDto>>> GetAll(int page, int take)
        {
            return await _service.GetAll(page, take);
        }
        [HttpGet("simple")]
        public async Task<ActionResult<DataCollection<ClienteSimpleDto>>> GetAllSimple(int page, int take)
        {
            return await _service.GetAllSimple(page, take);
        }
        [Authorize(Roles = RoleHelper.User + "," + RoleHelper.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetById(int id)
        {
            if (User.IsInRole(RoleHelper.Admin))
                return await _service.GetById(id);            
            var usuario = await _userManager.FindByIdAsync(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (usuario.IdCliente == id)
                return await _service.GetById(id);
            else
                return BadRequest();
        }
        [Authorize(Roles = RoleHelper.Admin)]
        [HttpPost]
        public async Task<ActionResult> Create(ClienteCreateOrUpdate model)
        {
            await _service.Create(model);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ClienteCreateOrUpdate model)
        {
            await _service.Update(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return Ok();
        }
    }
}
