using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Productos
{
    public class ProductoCategoria
    {
        [Key]
        public int IdProductoCategoria { get; set; }
        [Required]
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        [Required]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
    }
}
