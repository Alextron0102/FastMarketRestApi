using AutoMapper;
using FastMarketRESTAPI.Commons;
using FastMarketRESTAPI.Dto;
using FastMarketRESTAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Service.Productos
{
    public class ProductoServiceImpl : ProductoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductoServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(ProductoCreateOrUpdateDto model)
        {
            var entry = new Model.Productos.Producto
            {
                Nombre = model.Nombre,
                Stock = model.Stock,
                Precio = model.Precio,
                Imagen = model.Imagen,
            };
            await _context.Producto.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<DataCollection<ProductoSimpleDto>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<ProductoSimpleDto>>(
                await _context.Producto.OrderByDescending(x => x.IdProducto)
                .AsQueryable().PagedAsync(page, take)
                );
        }

        public async Task<DataCollection<ProductoUsuarioDto>> GetAllUsuario(int page, int take)
        {
            return _mapper.Map<DataCollection<ProductoUsuarioDto>>(
                await _context.Producto.OrderByDescending(x => x.IdProducto)
                .AsQueryable().PagedAsync(page, take)
                );
        }

        public async Task<ProductoDto> GetById(int id)
        {
            return _mapper.Map<ProductoDto>(
                await _context.Producto.Include(x => x.ProductoCategorias).ThenInclude(x => x.Categoria).SingleAsync(x => x.IdProducto == id)
                );
        }

        public async Task Remove(int id)
        {
            var entry = await _context.Producto.FindAsync(id);
            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, ProductoCreateOrUpdateDto model)
        {
            var entry = await _context.Producto.FindAsync(id);
            entry.Nombre = model.Nombre;
            entry.Stock = model.Stock;
            entry.Precio = model.Precio;
            entry.Imagen = model.Imagen;
            await _context.SaveChangesAsync();
        }
    }
}
