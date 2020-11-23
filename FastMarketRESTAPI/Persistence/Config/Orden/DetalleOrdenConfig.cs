using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Orden
{
    public class DetalleOrdenConfig
    {
        public DetalleOrdenConfig(EntityTypeBuilder<Model.Orden.DetalleOrden> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.IdDetalleOrden);
            entityTypeBuilder.HasOne(x => x.Orden).WithMany(x => x.DetalleOrdenes).HasForeignKey(x => x.IdOrden)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade)
                .IsRequired();
            entityTypeBuilder.HasOne(x => x.Producto).WithMany(x => x.DetalleOrden).HasForeignKey(x => x.IdProducto)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade)
                .IsRequired();
            entityTypeBuilder.Property(x => x.Cantidad).IsRequired();
        }
    }
}
