using FastMarketRESTAPI.Model.Orden;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Productos
{
    /// <summary>
    /// Clase producto, almacena todos los datos del producto
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Identificador del producto
        /// </summary>
        [Key]
        public int IdProducto { get; set; }
        /// <summary>
        /// Stock del producto
        /// </summary>
        [Required]
        public int Stock { get; set; }
        /// <summary>
        /// Nombre del producto
        /// </summary>
        [Required]
        public string Nombre { get; set; }
        /// <summary>
        /// Precio del producto
        /// </summary>
        [Required]
        public double Precio { get; set; }
        /// <summary>
        /// Imagen del producto(enlace)
        /// </summary>
        public string Imagen { get; set; }
        /// <summary>
        /// Las categorias a la que pertenece este producto
        /// </summary>
        public List<ProductoCategoria> ProductoCategorias { get; set; }
        /// <summary>
        /// Las ordenes a la que pertene este producto
        /// </summary>
        public List<DetalleOrden> DetalleOrden { get; set; }
    }
}
