using AutoMapper;
using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Orden
{
    public class OrdenServiceImpl : OrdenService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrdenServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(OrdenCreateOrUpdateDto model)
        {
            var cliente = await _context.Cliente.FindAsync(model.IdCliente);
            var entry = new Model.Orden.Orden
            {
                IdCliente = model.IdCliente,
                Fecha = Convert.ToDateTime(model.Fecha),
                EstadoPago = (Enums.EstadoPago)Enum.Parse(typeof(Enums.EstadoPago), model.EstadoPago, true),
                MontoPagado = model.MontoPagado.HasValue ? model.MontoPagado.Value : 0
            };
            //Creamos todos los detalles de orden:
            double acumulador = 0.0d;
            var productosAgregar = new List<Model.Orden.DetalleOrden>();
            foreach (var item in model.DetalleOrdenes)
            {
                //await _context.DetalleOrden.AddAsync(new Model.Orden.DetalleOrden
                //{
                //    IdOrden = entry.IdOrden,
                //    IdProducto = item.IdProducto,
                //    Cantidad = item.Cantidad
                //});
                productosAgregar.Add(new Model.Orden.DetalleOrden
                {
                    IdOrden = entry.IdOrden,
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad
                });
                //Buscamos el producto para sacar su precio y vamos acumulando lo que nos debe:
                var producto = await _context.Producto.FindAsync(item.IdProducto);
                acumulador += (item.Cantidad * producto.Precio);
            }
            if (entry.EstadoPago == Enums.EstadoPago.PagoParte ||
                entry.EstadoPago == Enums.EstadoPago.PorPagar)
            {
                //Tenemos que verificar que exista el id de la tasa para generar la deuda, caso contrario devolvemos error
                if (!model.IdTasa.HasValue) throw new Exception("No se introdujo tasa"); //rompemos la funcion
                //Rompemos la funcion si el cliente ya consumió por encima de su linea de credito
                if (cliente.LineaCredito < (cliente.LineaConsumida+acumulador)) throw new Exception("El cliente no cuenta con credito");
            }
            await _context.Orden.AddAsync(entry);
            await _context.SaveChangesAsync();
            foreach (var item in productosAgregar)
            {
                item.IdOrden = entry.IdOrden;
                await _context.DetalleOrden.AddAsync(item);
            }            
            //Creamos las deudas si es que el estado de pago es PorPagar
            if(entry.EstadoPago == Enums.EstadoPago.PorPagar)
            {
                await _context.Deuda.AddAsync(new Model.Deuda.Deuda
                {
                    IdOrden = entry.IdOrden,
                    //Que sólo el administrador pueda poner el id de otro cliente se valida en el mismo controller
                    IdCliente = model.IdCliente,
                    IdTasa = model.IdTasa.Value,
                    MontoInicial = acumulador,
                    MontoAcumulado = acumulador,//El mismo dia no acumuló nada
                    MontoInteres = acumulador,//El interes es 0 el mismo dia
                    EstadoDeuda = Enums.EstadoDeuda.Vigente,
                    FechaUltimaActualizacion = Convert.ToDateTime(model.Fecha)
                });
                //Actualizamos el estado de la linea de credito del cliente:                
                cliente.LineaConsumida += acumulador;              
            }
            else if(entry.EstadoPago == Enums.EstadoPago.PagoParte)
            {
                if(model.MontoPagado.HasValue)//Validamos que haya introducido un monto
                    if(model.MontoPagado.Value != 0.0d) //validamos que el monto introducido sea diferente de 0
                        acumulador -= model.MontoPagado.Value; //restamos el monto al total acumulado
                //Generamos la deuda:
                await _context.Deuda.AddAsync(new Model.Deuda.Deuda
                {
                    IdOrden = entry.IdOrden,
                    //Que sólo el administrador pueda poner el id de otro cliente se valida en el mismo controller
                    IdCliente = model.IdCliente,
                    IdTasa = model.IdTasa.Value,
                    MontoInicial = acumulador,
                    MontoAcumulado = acumulador,//El mismo dia no acumuló nada
                    MontoInteres = acumulador,//El interes es 0 el mismo dia
                    EstadoDeuda = Enums.EstadoDeuda.Vigente,
                    FechaUltimaActualizacion = Convert.ToDateTime(model.Fecha)
                });
                //Actualizamos el estado de la linea de credito del cliente:
                cliente.LineaConsumida += acumulador;
            }
            //En los otros 2 casos no se genera deuda.
            await _context.SaveChangesAsync();
        }       
        public async Task<DataCollection<OrdenSimpleDto>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<OrdenSimpleDto>>(
                await _context.Orden.Include(x=>x.Cliente).OrderByDescending(x => x.IdOrden)
                .AsQueryable().PagedAsync(page, take)
                );
        }
        public async Task<OrdenDto> GetById(int id)
        {
            return _mapper.Map<OrdenDto>(
                await _context.Orden.Include(x => x.DetalleOrdenes).FirstAsync(x => x.IdOrden == id)
                );
        }

        public async Task Remove(int id)
        {
            var entry = await _context.Orden.FindAsync(id);
            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }

    }
}
