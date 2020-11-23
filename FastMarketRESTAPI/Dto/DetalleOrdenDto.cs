using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Dto
{
    /// <summary>
    /// DetalleOrden creado por el IdProducto y cantidad, para el update se debe especificar el IdDetalleOrden
    /// </summary>
    public class DetalleOrdenCreateOrUpdateDto
    {
        public int? IdDetalleOrden { get; set; }
        [Required]
        public int IdProducto { get; set; }
        [Required]
        public int Cantidad { get; set; }
    }
    /// <summary>
    /// Llamado mediante la orden, muestra el producto y su cantidad
    /// </summary>
    public class DetalleOrdenDto
    {
        public int IdDetalleOrden { get; set; }
        //public int IdOrden { get; set; }->No es necesario porque siempre se va a llamar desde una orden
        public ProductoDto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
