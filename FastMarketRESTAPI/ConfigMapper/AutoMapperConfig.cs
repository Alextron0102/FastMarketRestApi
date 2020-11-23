using AutoMapper;
using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Model.Cliente;
using FastMarketRESTAPI.Model.Deuda;
using FastMarketRESTAPI.Model.Identity;
using FastMarketRESTAPI.Model.Orden;
using FastMarketRESTAPI.Model.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.ConfigMapper
{
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// Creacion del automapper para vincular las clases de model con los DTO
        /// </summary>
        public AutoMapperConfig()
        {
            //Clientes:            
            CreateMap<Cliente, ClienteDto>();
            CreateMap<DataCollection<Cliente>, DataCollection<ClienteDto>>();
            CreateMap<Cliente, ClienteSimpleDto>();
            CreateMap<DataCollection<Cliente>, DataCollection<ClienteSimpleDto>>();

            //Categoria:
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<Categoria, CategoriaSimpleDto>();
            CreateMap<DataCollection<Categoria>, DataCollection<CategoriaSimpleDto>>();
            CreateMap<Categoria, CategoriaProducto_CategoriaDto>();
            CreateMap<DataCollection<Categoria>, DataCollection<CategoriaProducto_CategoriaDto>>();
            CreateMap<Categoria, CategoriaProducto_ProductoDto>();
            CreateMap<DataCollection<Categoria>, DataCollection<CategoriaProducto_ProductoDto>>();

            //Producto:
            CreateMap<Producto, ProductoDto>();
            CreateMap<Producto, ProductoUsuarioDto>();
            CreateMap<DataCollection<Producto>, DataCollection<ProductoUsuarioDto>>();
            CreateMap<Producto, ProductoSimpleDto>();
            CreateMap<DataCollection<Producto>, DataCollection<ProductoSimpleDto>>();

            //Orden:
            CreateMap<Orden, OrdenDto>();
            CreateMap<Orden, OrdenSimpleDto>();
            CreateMap<DataCollection<Orden>, DataCollection<OrdenSimpleDto>>();

            //DetalleOrden:
            CreateMap<DetalleOrden, DetalleOrdenDto>();
            CreateMap<Producto, DetalleOrdenDto>();
            CreateMap<Orden, DetalleOrdenDto>();
            CreateMap<DataCollection<Orden>, DataCollection<DetalleOrdenDto>>();

            //Tasa:
            CreateMap<Tasa, TasaDto>();
            CreateMap<DataCollection<Tasa>, DataCollection<TasaDto>>();
            CreateMap<Tasa, TasaSimpleDto>();
            CreateMap<DataCollection<Tasa>, DataCollection<TasaSimpleDto>>();

            //Pagos:
            CreateMap<Pago, PagoDto>();
            CreateMap<DataCollection<Pago>, DataCollection<PagoDto>>();
            CreateMap<List<Pago>, List<PagoDto>>();
            //Pagos:
            CreateMap<Pago, PagoDto>();
            //Deudas:
            CreateMap<Deuda, DeudaSimpleDto>();
            CreateMap<Cliente, DeudaSimpleDto>();
            CreateMap<Orden, DeudaSimpleDto>();
            CreateMap<DataCollection<Deuda>, DataCollection<DeudaSimpleDto>>();

            CreateMap<Deuda, DeudaDto>();
            CreateMap<DataCollection<Deuda>, DataCollection<DeudaDto>>();
            CreateMap<Cliente, DeudaDto>();
            CreateMap<Orden, DeudaDto>();
            CreateMap<Tasa, DeudaDto>();
            CreateMap<List<Producto>,DeudaDto>();



            //Identity:
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(
                    dest => dest.Roles,
                    opts => opts.MapFrom(src => src.UserRoles.Select(y => y.Role.Name).ToList())
                    );
        }
    }
}
