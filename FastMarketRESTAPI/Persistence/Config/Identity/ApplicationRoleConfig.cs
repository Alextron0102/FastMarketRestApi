using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Model.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Persistence.Config.Identity
{
    public class ApplicationRoleConfig
    {
        public ApplicationRoleConfig(EntityTypeBuilder<ApplicationRole> entityBuilder)
        {
            entityBuilder.HasMany(x => x.UserRoles).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).IsRequired();
            //Aqui insertamos los roles de RoleHelper a nuestra DB
            var properties = typeof(RoleHelper).GetFields();
            foreach (var item in properties)
            {
                entityBuilder.HasData(
                   new ApplicationRole
                   {
                       Id = Guid.NewGuid().ToString(),
                       Name = item.GetValue(null).ToString(),
                       NormalizedName = item.GetValue(null).ToString()
                   });
            }
            //entityBuilder.HasData(
            //    new ApplicationRole
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name = RoleHelper.Admin,
            //        NormalizedName = RoleHelper.Admin
            //    },
            //    new ApplicationRole
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name = RoleHelper.User,
            //        NormalizedName = RoleHelper.User
            //    }
            //    );
        }
    }
}
