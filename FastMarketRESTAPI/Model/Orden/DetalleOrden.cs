using FastMarketRESTAPI.Model.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Orden
{
    public class DetalleOrden
    {
        [Key]
        public int IdDetalleOrden { get; set; }
        [Required]
        public int IdOrden { get; set; }
        public Orden Orden { get; set; }
        [Required]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
    }
}
