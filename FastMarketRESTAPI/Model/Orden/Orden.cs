using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Model.Deuda;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastMarketRESTAPI.Commons.Enums;

namespace FastMarketRESTAPI.Model.Orden
{
    public class Orden
    {
        [Key]
        public int IdOrden { get; set; }
        [Required]
        public int IdCliente { get; set; }
        public Cliente.Cliente Cliente { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public EstadoPago EstadoPago { get; set; }
        public double? MontoPagado { get; set; }//? porque el estado puede anular este campo
        public List<DetalleOrden> DetalleOrdenes { get; set; }
        public List<Deuda.Deuda> Deudas { get; set; }
    }
}
