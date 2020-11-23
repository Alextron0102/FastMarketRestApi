
using FastMarketRESTAPI.Model.Cliente;
using FastMarketRESTAPI.Model.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Identity
{
    public class ApplicationUserConfig
    {
        public ApplicationUserConfig(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired();
            entityBuilder.Property(x => x.Nombres).IsRequired().HasMaxLength(60);
            entityBuilder.Property(x => x.Apellidos).IsRequired().HasMaxLength(100);
            entityBuilder.HasOne(x => x.Cliente).WithOne(x => x.Usuario).HasForeignKey<ApplicationUser>(x => x.IdCliente)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);//=>Si se elimina el cliente, el IdCliente se vuelve null
        }
    }
}
