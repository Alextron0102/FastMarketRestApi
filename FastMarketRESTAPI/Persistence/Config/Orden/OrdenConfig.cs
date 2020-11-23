using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastMarketRESTAPI.Model.Orden;
using FastMarketRESTAPI.Commons;

namespace FastMarketRESTAPI.Persistence.Config.Orden
{
    public class OrdenConfig
    {
        public OrdenConfig(EntityTypeBuilder<Model.Orden.Orden> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.IdOrden);
            entityTypeBuilder.HasOne(x => x.Cliente).WithMany(x => x.Ordenes).HasForeignKey(x => x.IdCliente).IsRequired();
            entityTypeBuilder.Property(x => x.Fecha).IsRequired();
            entityTypeBuilder.Property(x => x.EstadoPago)
                .HasConversion(x => x.ToString(), 
                x => (Enums.EstadoPago)Enum.Parse(typeof(Enums.EstadoPago), x))
                .IsRequired();
            entityTypeBuilder.Property(x => x.MontoPagado);
        }
    }
}
