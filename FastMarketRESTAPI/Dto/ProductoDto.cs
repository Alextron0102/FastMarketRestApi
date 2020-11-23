using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Dto
{
    /// <summary>
    /// PAra la creacion o actualizacion de las ordenes
    /// </summary>
    public class ProductoCreateOrUpdateDto
    {
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public double Precio { get; set; }
        public string Imagen { get; set; }//Por si se desea mostrar una imagen al momento de comprar.

    }
    /// <summary>
    /// Para poder devolver los datos del producto
    /// </summary>
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public string Nombre { get; set; }
        /// <summary>
        /// Muestra el precio del producto
        /// </summary>
        public double Precio { get; set; }
        /// <summary>
        /// Si se desea mostrar una imagen del producto, indicamos el enlace de este.
        /// </summary>
        public string Imagen { get; set; }
        /// <summary>
        /// Muestra las categorias a las que pertenece este producto
        /// </summary>
        public List<CategoriaProducto_ProductoDto> ProductoCategorias { get; set; }
    }
    public class ProductoUsuarioDto
    {
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public string Nombre { get; set; }
        /// <summary>
        /// Muestra el precio del producto
        /// </summary>
        public double Precio { get; set; }
        /// <summary>
        /// Si se desea mostrar una imagen del producto, indicamos el enlace de este.
        /// </summary>
        public string Imagen { get; set; }
    }
    /// <summary>
    /// Para el listado de productos
    /// </summary>
    public class ProductoSimpleDto
    {
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int IdProducto { get; set; }
        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string Nombre { get; set; }
    }

}
