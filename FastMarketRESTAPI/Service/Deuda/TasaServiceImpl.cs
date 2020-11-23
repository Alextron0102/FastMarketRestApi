using AutoMapper;
using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Deuda
{
    public class TasaServiceImpl : TasaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TasaServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(TasaCreateOrUpdateDto model)
        {
            var entry = new Model.Deuda.Tasa
            {
                Valor = model.Valor,
                TipoTasa = (Enums.TipoTasa)Enum.Parse(typeof(Enums.TipoTasa), model.TipoTasa, true),
                Descripcion = model.Descripcion,
                Publica = false
            };
            await _context.Tasa.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<DataCollection<TasaDto>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<TasaDto>>(
                await _context.Tasa.OrderByDescending(x => x.IdTasa)
                .AsQueryable().PagedAsync(page, take)
                );
        }

        public async Task<DataCollection<TasaSimpleDto>> GetAllSimple(int page, int take)
        {
            return _mapper.Map<DataCollection<TasaSimpleDto>>(
                await _context.Tasa.OrderByDescending(x => x.IdTasa).AsQueryable().PagedAsync(page,take)
                ); ;
        }

        public async Task<TasaDto> GetById(int id)
        {
            return _mapper.Map<TasaDto>(
                await _context.Tasa.FindAsync(id)
                );
        }

        public async Task Remove(int id)
        {
            var entry = await _context.Tasa.FindAsync(id);
            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, TasaCreateOrUpdateDto model)
        {
            var entry = await _context.Tasa.FindAsync(id);
            entry.Valor = model.Valor;
            entry.TipoTasa = (Enums.TipoTasa)Enum.Parse(typeof(Enums.TipoTasa), model.TipoTasa, true);
            entry.Descripcion = model.Descripcion;
            await _context.SaveChangesAsync();
        }
    }
}
