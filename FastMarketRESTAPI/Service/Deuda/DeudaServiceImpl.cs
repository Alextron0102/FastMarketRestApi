using AutoMapper;
using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Model.Deuda;
using FastMarketRESTAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Deuda
{
    public class DeudaServiceImpl : DeudaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeudaServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DataCollection<DeudaSimpleDto>> GetAll(int page, int take)
        {
            var deudas = await _context.Deuda.Include(x => x.Cliente).Include(x => x.Orden).OrderByDescending(x => x.IdDeuda)
                .AsQueryable().PagedAsync(page, take);
                //aqui además de devolver las deudas, se tiene que actualizar sus montos:
            return _mapper.Map<DataCollection<DeudaSimpleDto>>(
                    deudas
                );
        }

        public async Task<DeudaDto> GetById(int id)
        {
            var deuda = await _context.Deuda
                .Include(x => x.Cliente)
                .Include(x => x.Tasa)
                .Include(x => x.Pagos)
                .Include(x => x.Orden).ThenInclude(x => x.DetalleOrdenes).ThenInclude(x => x.Producto)
                .SingleAsync(x => x.IdDeuda == id);
            deuda = ActualizarMontos(deuda);
            await _context.SaveChangesAsync();
            return _mapper.Map<DeudaDto>(deuda);
        }
        public async Task<List<PagoDto>> GetPagosById(int id)
        {
            var pagos = await _context.Pago.Where(x => x.IdDeuda == id).ToListAsync();
            List<PagoDto> aux = new List<PagoDto>();
            foreach (var item in pagos)
            {
                aux.Add(_mapper.Map<PagoDto>(item));
            }
            return aux;           
        }

        public async Task Update(int id, DeudaUpdateDto model)
        {
            var entry = await _context.Deuda.FirstAsync(x=>x.IdDeuda == id);
            if (entry.EstadoDeuda == Enums.EstadoDeuda.Cancelada) return;//No procesamos nada
            if(model.IdTasa.HasValue)
                entry.IdTasa = model.IdTasa.Value;
            if (model.IdCliente.HasValue)
                entry.IdCliente = model.IdCliente.Value;
            if(model.Pagos.Count > 0)//Validamos siempre que no estemos recibiendo un arreglo vacio
            {
                //No podemos eliminar pagos, simplemente agregamos los nuevos pagos o modificamos los existentes:
                foreach (var item in model.Pagos)
                {
                    await _context.Pago.AddAsync(new Pago
                    {
                        IdDeuda = entry.IdDeuda,
                        MontoPago = item.MontoPago,
                        Fecha = Convert.ToDateTime(item.Fecha)
                    });
                    entry.MontoAcumulado -= item.MontoPago;
                    if (entry.MontoAcumulado <= 0) entry.EstadoDeuda = Enums.EstadoDeuda.Cancelada;
                }
            }
            await _context.SaveChangesAsync();
        }
        public Model.Deuda.Deuda ActualizarMontos(Model.Deuda.Deuda deuda)
        {
            int diastranscurridos = Math.Abs((DateTime.Today - deuda.FechaUltimaActualizacion).Days);
            if (diastranscurridos == 0) return deuda;
            if(deuda.Tasa.TipoTasa == Enums.TipoTasa.TasaNominalMensual)
            {
                var aux = TNMaTED(deuda.Tasa.Valor, diastranscurridos);
                deuda.MontoAcumulado = Math.Round( deuda.MontoAcumulado * (1+ aux),2);
            }
            else if(deuda.Tasa.TipoTasa == Enums.TipoTasa.TasaEfectivaMensual)
            {
                var aux = TEMaTED(deuda.Tasa.Valor, diastranscurridos);
                deuda.MontoAcumulado = Math.Round(deuda.MontoAcumulado * (1 + aux),2);
            }
            deuda.FechaUltimaActualizacion = DateTime.Today;
            return deuda;//Devolvemos la deuda actualizada
        }
        public static double TNMaTED(double tasa, int tiempoDias)
        {
            double m = 30.0d;//30.0d porque es mensual
            double n = tiempoDias;
            return (Math.Pow(1 + ((tasa / 100.0d) / m), n) - 1);
        }
        public static double TEMaTED(double tasa,int tiempoDias)
        {
            return Math.Pow(1 + tasa / 100.0d, tiempoDias / 30.0d) - 1;
        }
    }
}
