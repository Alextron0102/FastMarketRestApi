using FastMarketRESTAPI.Model.Productos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Productos
{
    public class ProductoConfig
    {
        public ProductoConfig(EntityTypeBuilder<Producto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.IdProducto);
            entityBuilder.Property(x => x.Imagen);
            entityBuilder.Property(x => x.Stock).IsRequired();
            entityBuilder.Property(x => x.Nombre).HasMaxLength(50).IsRequired();
            entityBuilder.Property(x => x.Precio).IsRequired();
        }
    }
}
