using AutoMapper.Configuration;
using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public IdentityController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        [Authorize(Roles=RoleHelper.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(email));
            return Ok();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Create(ApplicationUserRegisterDto model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                IdCliente = null
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            var check = await _userManager.CheckPasswordAsync(user, model.Password);
            if (check)
            {
                await _userManager.AddToRoleAsync(user, RoleHelper.User);
                return Ok();
            }
            else
            {
                return BadRequest("No se pudo crear el usuario.");
            }
        }
        [HttpPost("admin/register")]
        public async Task<IActionResult> CreateAdmin(ApplicationUserRegisterDto model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                IdCliente = null
            };
            await _userManager.CreateAsync(user, model.Password);
            var check = await _userManager.CheckPasswordAsync(user, model.Password);
            if (check)
            {
                //significa que se creo el usuario, y su contraseña es valida:
                await _userManager.AddToRoleAsync(user, RoleHelper.Admin);
                return Ok();
            }
            else
            {
                return BadRequest("No se pudo crear el usuario.");
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var check = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (check.Succeeded)
                {
                    return Ok(
                        await GenerateToken(user)
                    );
                }
            }
            return BadRequest("Acceso no valido al sistema.");
        }
        [Authorize]
        [HttpGet("refresh_token")]
        public async Task<IActionResult> Refresh()
        {
            var userId = User.Claims.Where(x =>
                x.Type.Equals(ClaimTypes.NameIdentifier)
            ).Single().Value;

            var user = await _userManager.FindByIdAsync(userId);

            return Ok(
                await GenerateToken(user)
            );
        }
        private async Task<string> GenerateToken(ApplicationUser user)
        {
            //Definimos un secretKey
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            //Los Claims contiene información relevante de nuestro usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),//Asociar el id del usuario
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Nombres),
                new Claim(ClaimTypes.Surname, user.Apellidos)
            };
            //Roles
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role)
                );
            }

            //Contiene la información del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Los Claims contiene información relevante de nuestro usuario
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),//Duración de un dia a partir de hoy
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }

    }
}
