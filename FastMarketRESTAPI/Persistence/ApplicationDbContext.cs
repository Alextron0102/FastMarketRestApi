using FastMarketRESTAPI.Model.Cliente;
using FastMarketRESTAPI.Model.Deuda;
using FastMarketRESTAPI.Model.Identity;
using FastMarketRESTAPI.Model.Orden;
using FastMarketRESTAPI.Model.Productos;
using FastMarketRESTAPI.Persistence.Config.Cliente;
using FastMarketRESTAPI.Persistence.Config.Deuda;
using FastMarketRESTAPI.Persistence.Config.Identity;
using FastMarketRESTAPI.Persistence.Config.Orden;
using FastMarketRESTAPI.Persistence.Config.Productos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FastMarketRESTAPI.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        string,
        IdentityUserClaim<string>,
        ApplicationUserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //TODO: Poner tablas:
        public DbSet<Model.Cliente.Cliente> Cliente { get; set; }
        public DbSet<Model.Orden.Orden> Orden { get; set; }
        public DbSet<DetalleOrden> DetalleOrden { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<ProductoCategoria> ProductoCategoria { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Tasa> Tasa { get; set; }
        public DbSet<Model.Deuda.Deuda> Deuda { get; set; }
        public DbSet<Pago> Pago { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new ClienteConfig(builder.Entity<Model.Cliente.Cliente>());
            new OrdenConfig(builder.Entity<Model.Orden.Orden>());
            new DetalleOrdenConfig(builder.Entity<DetalleOrden>());
            new CategoriaConfig(builder.Entity<Categoria>());
            new ProductoCategoriaConfig(builder.Entity<ProductoCategoria>());
            new ProductoConfig(builder.Entity<Producto>());
            new TasaConfig(builder.Entity<Tasa>());
            new DeudaConfig(builder.Entity<Model.Deuda.Deuda>());
            new ApplicationUserConfig(builder.Entity<ApplicationUser>());
            new ApplicationRoleConfig(builder.Entity<ApplicationRole>());
            new PagoConfig(builder.Entity<Pago>());
        }
    }
}
