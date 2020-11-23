using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FastMarketRESTAPI.Model.Identity;
using FastMarketRESTAPI.Persistence;
using FastMarketRESTAPI.Service.Cliente;
using FastMarketRESTAPI.Service.Deuda;
using FastMarketRESTAPI.Service.Orden;
using FastMarketRESTAPI.Service.Productos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FastMarketRESTAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //// Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FastMarketAPI",
                    Version = "v1",
                    Description = "Api rest para FastMarket",
                });
            });
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(
               //opts => opts.UseNpgsql(Configuration.GetConnectionString("LocalConnection"))
               opts => opts.UseNpgsql(Configuration.GetConnectionString("HerokuConnection"))
            );
            ////Para la seguridad:
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            ////Para hacer la contraseña especial:
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddTransient<DeudaService, DeudaServiceImpl>();
            services.AddTransient<OrdenService, OrdenServiceImpl>();
            services.AddTransient<ClienteService, ClienteServiceImpl>();
            services.AddTransient<CategoriaService, CategoriaServiceImpl>();
            services.AddTransient<ProductoService, ProductoServiceImpl>();
            services.AddTransient<TasaService, TasaServiceImpl>();
            services.AddCors(options =>
            {
                options.AddPolicy("Cors",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });
            //Configuracion de que la autentitacion es a traves de un token tipo Bearer
            //Leemos el secretkey
            var key = Encoding.ASCII.GetBytes(
              Configuration.GetValue<string>("SecretKey")
            );
            //Definimos Schema de autentificacion con JwtBearer(json web token bir)
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //Se configura el token para indicar como sera leido
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; //Queremos validar si las peticiones validar si vienen de https en produccion pueden colocar si viene desde un SSL
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),//este debe contener la información del secret key
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FastMarketAPI");
                c.RoutePrefix = string.Empty;                
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Cors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
