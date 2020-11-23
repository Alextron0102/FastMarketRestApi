using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Dto
{
    public class CategoriaCreateOrUpdateDto
    {
        /// <summary>
        /// Indica el nombre de la categoria
        /// </summary>
        [Required]        
        public string Nombre { get; set; }
        /// <summary>
        /// Puede como no puede haber descripcion de la categoria
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Al momento de crear o actualizar los productos que pertenecen a esta categoria, 
        /// podemos hacerlo desde la categoria o desde el producto,
        /// en este caso indicamos los id de los productos que vamos a añadir a esta categoria
        /// </summary>
        public List<int> IdProductos { get; set; }
    }
    public class CategoriaDto
    {
        /// <summary>
        /// Indica el id de categoria
        /// </summary>
        public int IdCategoria { get; set; }
        /// <summary>
        /// Indica el nombre de la categoria
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Indica la descripcion de la categoria
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Indica los productos que pertenecen a esta categoria
        /// </summary>
        public List<CategoriaProducto_CategoriaDto> ProductoCategorias { get; set; }
    }
    public class CategoriaSimpleDto
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
    }
    /// <summary>
    /// La relacion de CategoriaProducto para categoria
    /// </summary>
    public class CategoriaProducto_CategoriaDto
    {
        public int IdProductoCategoria { get; set; }
        public ProductoSimpleDto Producto { get; set; }
    }
    /// <summary>
    /// La relacion de CategoriaProducto para producto
    /// </summary>
    public class CategoriaProducto_ProductoDto
    {
        public int IdProductoCategoria { get; set; }
        public CategoriaSimpleDto Producto { get; set; }
    }
}
