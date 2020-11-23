using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Deuda
{
    /// <summary>
    /// Un cliente puede ir pagando su deuda por partes, cada uno de los pagos se deben de registrar en la bd
    /// </summary>
    public class Pago
    {
        [Required]
        public int IdPago { get; set; }
        [Required]
        public int IdDeuda { get; set; }
        public Deuda Deuda { get; set; }
        [Required]
        public double MontoPago { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}
