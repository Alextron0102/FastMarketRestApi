using FastMarketRESTAPI.Model.Productos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Productos
{
    public class CategoriaConfig
    {
        public CategoriaConfig(EntityTypeBuilder<Categoria> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.IdCategoria);
            entityTypeBuilder.Property(x => x.Nombre).HasMaxLength(50).IsRequired();
            entityTypeBuilder.Property(x => x.Descripcion).HasMaxLength(250);
        }
    }
}
