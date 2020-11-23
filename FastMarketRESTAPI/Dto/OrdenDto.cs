using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastMarketRESTAPI.Commons.Enums;

namespace FastMarketRESTAPI.Dto
{
    /// <summary>
    /// Para la creacion o actualizacion de las ordenes
    /// </summary>
    public class OrdenCreateOrUpdateDto
    {
        public int IdCliente { get; set; }
        //public DateTime Fecha { get; set; }-> se genera del sistema
        [Required]
        public string EstadoPago { get; set; }//-> se convierte a enum dentro de los services
        public double? MontoPagado { get; set; }//Si el estado de pago es pagado, directamente se crea el montopagado
        public List<DetalleOrdenCreateOrUpdateDto> DetalleOrdenes { get; set; }
        public int? IdTasa { get; set; }//La tasa es definida por el administrador
        public string Fecha { get; set; }//Se recibe como yyyy-mm-dd
    }
    /// <summary>
    /// Lo que vamos a mostrar al usuario para la creacion de su orden
    /// </summary>
    public class OrdenDto
    {
        public int IdOrden { get; set; }
        public ClienteSimpleDto Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoPago { get; set; }
        public double? MontoPagado { get; set; }
        public List<DetalleOrdenDto> DetalleOrdenes { get; set; }
    }
    /// <summary>
    /// Para el listado de las ordenes
    /// </summary>
    public class OrdenSimpleDto
    {
        public int IdOrden { get; set; }
        public ClienteSimpleDto Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoPago { get; set; }
    }
}
