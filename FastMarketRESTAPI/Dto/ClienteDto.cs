using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Dto
{
    /// <summary>
    /// Para la creacion o actualizacion de los datos del cliente
    /// <para>
    /// Se crea en 2 casos: Cuando el cliente no tiene usuario y va a la tienda a comprar(el administrador lo crea)
    /// </para><para>
    /// O que el mismo usuario cliente cree sus propios datos(en la clase Cliente)
    /// </para> 
    /// </summary>
    public class ClienteCreateOrUpdate
    {
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Dni { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        public double LineaCredito { get; set; }
    }
    /// <summary>
    /// Para mostrar los datos completos del cliente
    /// </summary>
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Dni { get; set; }
        public string Nombres { get; set; }//Sacamos los nombres directamente del usuario
        public string Apellidos { get; set; }//Sacamos los apellidos directamente del usuario
        public double LineaCredito { get; set; }
        public double LineaConsumida { get; set; }
    }
    /// <summary>
    /// Para el listado de clientes
    /// </summary>
    public class ClienteSimpleDto
    {
        public int IdCliente { get; set; }
        public string Nombres { get; set; }//Sacamos los nombres directamente del usuario
        public string Apellidos { get; set; }//Sacamos los apellidos directamente del usuario
    }
}
