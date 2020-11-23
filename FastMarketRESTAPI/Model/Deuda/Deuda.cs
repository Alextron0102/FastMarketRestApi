using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Model.Orden;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Deuda
{
    public class Deuda
    {
        [Key]
        public int IdDeuda { get; set; }
        [Required]
        public int IdOrden { get; set; }
        public Orden.Orden Orden { get; set; }
        [Required]
        public int IdCliente { get; set; }
        public Cliente.Cliente Cliente { get; set; }
        [Required]
        public int IdTasa { get; set; }
        public Tasa Tasa { get; set; }
        [Required]
        public double MontoInicial { get; set; }//El monto por el cual se genera la deuda
        [Required]
        public double MontoAcumulado { get; set; }//El monto que se tiene que pagar
        [Required]
        public double MontoInteres { get; set; }//El monto que se genera por los intereses.
        public DateTime? FechaDePago { get; set; }//La fecha de pago de la deuda
        public DateTime FechaUltimaActualizacion { get; set; }//Para actualizar el monto que se debe de pagar
        [Required]
        public Enums.EstadoDeuda EstadoDeuda { get; set; }
        public List<Pago> Pagos { get; set; }
    }
}
