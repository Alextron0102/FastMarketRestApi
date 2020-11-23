using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Dto
{
    /// <summary>
    /// Muestra un pago de una deuda
    /// <para>
    /// Siempre va a ser llamado desde DeudaDto
    /// </para>
    /// </summary>
    public class PagoDto
    {
        public int IdPago { get; set; }
        public double MontoPago { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class PagoCreateOrUpdateDto
    {
        public double MontoPago { get; set; }
        public string Fecha { get; set; }
    }
}
