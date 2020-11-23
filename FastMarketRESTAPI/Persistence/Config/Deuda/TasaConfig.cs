using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Model.Deuda;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Deuda
{
    public class TasaConfig
    {
        public TasaConfig(EntityTypeBuilder<Tasa> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.IdTasa);
            entityTypeBuilder.Property(x => x.Valor).IsRequired();
            entityTypeBuilder.Property(x => x.TipoTasa)
                .HasConversion(x => x.ToString(),
                x => (Enums.TipoTasa)Enum.Parse(typeof(Enums.TipoTasa), x))
                .IsRequired();
            entityTypeBuilder.Property(x => x.Descripcion).HasMaxLength(250);
            entityTypeBuilder.Property(x => x.Publica).IsRequired();
        }
    }
}
