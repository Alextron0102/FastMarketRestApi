using FastMarketRESTAPI.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Deuda
{
    public class DeudaConfig
    {
        public DeudaConfig(EntityTypeBuilder<Model.Deuda.Deuda> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.IdDeuda);
            entityTypeBuilder.HasOne(x => x.Orden).WithMany(x => x.Deudas).HasForeignKey(x => x.IdOrden).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            entityTypeBuilder.HasOne(x => x.Cliente).WithMany(x => x.Deudas).HasForeignKey(x => x.IdCliente).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            entityTypeBuilder.HasOne(x => x.Tasa).WithMany(x => x.Deudas).HasForeignKey(x => x.IdTasa).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            entityTypeBuilder.Property(x => x.MontoInicial).IsRequired();
            entityTypeBuilder.Property(x => x.MontoAcumulado).IsRequired();
            entityTypeBuilder.Property(x => x.MontoInteres).IsRequired();
            entityTypeBuilder.Property(x => x.FechaDePago);
            entityTypeBuilder.Property(x => x.FechaUltimaActualizacion);
            entityTypeBuilder.Property(x => x.EstadoDeuda)
                .HasConversion(x => x.ToString(),
                x => (Enums.EstadoDeuda)Enum.Parse(typeof(Enums.EstadoDeuda), x))
                .IsRequired();
        }
    }
}
