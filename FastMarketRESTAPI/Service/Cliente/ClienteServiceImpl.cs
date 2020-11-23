using AutoMapper;
using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Model.Cliente;
using FastMarketRESTAPI.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Cliente
{
    public class ClienteServiceImpl : ClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClienteServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(ClienteCreateOrUpdate model)
        {
            var cliente = await _context.Cliente.AddAsync(new Model.Cliente.Cliente
            {
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                Dni = model.Dni,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                LineaCredito = model.LineaCredito,
                LineaConsumida = 0
            });
            await _context.SaveChangesAsync();
        }
        public async Task<DataCollection<ClienteDto>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<ClienteDto>>(
                await _context.Cliente.OrderByDescending(x => x.IdCliente)
                .AsQueryable().PagedAsync(page, take)
                );
        }
        public async Task<DataCollection<ClienteSimpleDto>> GetAllSimple(int page, int take)
        {
            return _mapper.Map<DataCollection<ClienteSimpleDto>>(
                await _context.Cliente.OrderByDescending(x => x.IdCliente)
                .AsQueryable().PagedAsync(page,take)
                );
        }

        public async Task<ClienteDto> GetById(int id)
        {
            return _mapper.Map<ClienteDto>(
                await _context.Cliente.FindAsync(id)
                );
        }

        public async Task Remove(int id)
        {
            var entry = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(entry);
            await _context.SaveChangesAsync();
        }
        public async Task Update(int id, ClienteCreateOrUpdate model)
        {
            var entry = await _context.Cliente.FindAsync(id);
            //Siempre tenemos que validar que el campo no esté vacio, si no lo está, actualizamos el valor
            if (model.Direccion != "")
                entry.Direccion = model.Direccion;
            if (model.Nombres != "")
                entry.Nombres = model.Nombres;
            if (model.Apellidos != "")
                entry.Apellidos = model.Apellidos;
            if (model.Telefono != "")
                entry.Telefono = model.Telefono;
            if (model.Dni != "")
                entry.Dni = model.Dni;
            entry.LineaCredito = model.LineaCredito;
            await _context.SaveChangesAsync();
        }
    }
}
