using FastMarketRESTAPI.Model.Identity;
using FastMarketRESTAPI.Model.Orden;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Cliente
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Dni { get; set; }
        [Required]
        public string Nombres { get; set; }//Sacamos los nombres directamente del usuario
        [Required]
        public string Apellidos { get; set; }//Sacamos los apellidos directamente del usuario
        public double LineaCredito { get; set; }
        public double LineaConsumida { get; set; }
        public List<Orden.Orden> Ordenes { get; set; }
        public List<Deuda.Deuda> Deudas { get; set; }
        //public string IdUsario { get; set; }
        public ApplicationUser Usuario { get; set; }

    }
}
