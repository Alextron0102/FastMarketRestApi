using FastMarketRESTAPI.Model.Deuda;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Deuda
{
    public class PagoConfig
    {
        public PagoConfig(EntityTypeBuilder<Pago> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.IdPago);
            //Si se elimina la deuda, se eliminan los pagos
            entityTypeBuilder.HasOne(t => t.Deuda).WithMany(t => t.Pagos).HasForeignKey(t => t.IdDeuda).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade).IsRequired();
            entityTypeBuilder.Property(t => t.Fecha);
            entityTypeBuilder.Property(t => t.MontoPago).IsRequired();
        }
    }
}
