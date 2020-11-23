using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Cliente
{
    public class ClienteConfig
    {
        public ClienteConfig(EntityTypeBuilder<Model.Cliente.Cliente> entityBuilder) 
        {
            entityBuilder.HasKey(x => x.IdCliente);
            //Los 3 datos del cliente son obligatorios
            entityBuilder.Property(x => x.Direccion).HasMaxLength(255).IsRequired();
            entityBuilder.Property(x => x.Telefono).HasMaxLength(15).IsRequired();
            entityBuilder.Property(x => x.Dni).HasMaxLength(8).IsRequired();
            entityBuilder.Property(x => x.Nombres).HasMaxLength(60).IsRequired();
            entityBuilder.Property(x => x.Apellidos).HasMaxLength(100).IsRequired();
            entityBuilder.Property(x => x.LineaCredito);
            entityBuilder.Property(x => x.LineaCredito);
            //entityBuilder.HasOne(x => x.Usuario).WithOne(x => x.Cliente).HasForeignKey<Model.Cliente.Cliente>(x => x.IdUsario).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
