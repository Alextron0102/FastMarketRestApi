using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Productos
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<ProductoCategoria> ProductoCategorias { get; set; }
    }
}
