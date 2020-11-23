using FastMarketRESTAPI.Model.Productos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Productos
{
    public class ProductoCategoriaConfig
    {
        public ProductoCategoriaConfig(EntityTypeBuilder<ProductoCategoria> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.IdProductoCategoria);
            //Señalamos las FK y indicamos que se eliminen las filas si se eliminan los productos o las categorias
            entityTypeBuilder.HasOne(x => x.Producto).WithMany(x => x.ProductoCategorias).HasForeignKey(x => x.IdProducto)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            entityTypeBuilder.HasOne(x => x.Categoria).WithMany(x => x.ProductoCategorias).HasForeignKey(x => x.IdCategoria)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
