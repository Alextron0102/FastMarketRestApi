using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        //El usuario tiene que estar vinculado con el cliente:
        public int? IdCliente { get; set; }//En el  caso el usuario sea el administrador no tendrá un cliente vinculado
        public virtual Cliente.Cliente Cliente { get; set; }
    }
}
