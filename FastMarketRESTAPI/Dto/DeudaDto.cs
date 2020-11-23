using FastMarketRESTAPI.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Dto
{
    /// <summary>
    /// Para actualizar la tasa de la deuda, la fecha y el estado de esta.
    /// </summary>
    public class DeudaUpdateDto
    {
        public int? IdTasa { get; set; }//Por si se desea cambiar la tasa
        public int? IdCliente { get; set; }//Por si se le desea cargar la deuda a otra persona
        public List<PagoCreateOrUpdateDto> Pagos { get; set; }
    }
    /// <summary>
    /// Los datos detallados que se le dan al usuario sobre la deuda
    /// </summary>
    public class DeudaDto
    {
        public int IdDeuda { get; set; }
        public OrdenDto Orden { get; set; }
        public ClienteDto Cliente { get; set; }
        public TasaDto Tasa { get; set; }
        public double MontoInicial { get; set; }//El monto por el cual se genera la deuda
        public double MontoAcumulado { get; set; }//El monto que se tiene que pagar
        public double MontoInteres { get; set; }//El monto que se genera por los intereses.
        public DateTime? FechaDePago { get; set; }//La fecha de pago de la deuda
        public string EstadoDeuda { get; set; }
        public List<PagoDto> Pagos { get; set; }
    }
    /// <summary>
    /// Los datos simple para un listado fácil
    /// </summary>
    public class DeudaSimpleDto
    {
        public int IdDeuda { get; set; }
        public OrdenSimpleDto Orden { get; set; }
        public ClienteSimpleDto Cliente { get; set; }
        public string EstadoDeuda { get; set; }
    }

}
