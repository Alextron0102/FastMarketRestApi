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
    public class CategoriaServiceImpl : CategoriaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriaServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Create(CategoriaCreateOrUpdateDto model)
        {
            var entry = new Model.Productos.Categoria
            {
                Nombre= model.Nombre,
                Descripcion= model.Descripcion,
            };
            await _context.Categoria.AddAsync(entry);
            await _context.SaveChangesAsync();
            //Ahora creamos las relaciones con los productos:
            foreach (var item in model.IdProductos)
            {
                await _context.ProductoCategoria.AddAsync(new Model.Productos.ProductoCategoria
                {
                    IdCategoria = entry.IdCategoria,
                    IdProducto = item
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<DataCollection<CategoriaSimpleDto>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<CategoriaSimpleDto>>(
                await _context.Categoria.OrderByDescending(x => x.IdCategoria)
                .AsQueryable().PagedAsync(page, take)
                );
        }

        public async Task<CategoriaDto> GetById(int id)
        {
            return _mapper.Map<CategoriaDto>(
                await _context.Categoria.Include(x => x.ProductoCategorias).ThenInclude(x => x.Producto).SingleAsync(x => x.IdCategoria == id)
                );
        }

        public async Task Remove(int id)
        {
            var entry = await _context.Categoria.FindAsync(id);
            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, CategoriaCreateOrUpdateDto model)
        {
            var entry = await _context.Categoria.Include(x=>x.ProductoCategorias).SingleAsync(x=>x.IdCategoria == id);
            entry.Nombre = model.Nombre;
            entry.Descripcion = model.Descripcion;
            //Tenemos que actualizar todos los productos que estan relacionados con la categoria, para eso:
            //Primero analizamos los nuevos productos:
            var antiguos = entry.ProductoCategorias.Select(x => x.IdProducto);
            var nuevos = model.IdProductos.Except(antiguos);
            foreach (var item in nuevos)
            {
                await _context.ProductoCategoria.AddAsync(new Model.Productos.ProductoCategoria
                {
                    IdCategoria = entry.IdCategoria,
                    IdProducto = item,
                });
            }
            var eliminar = antiguos.Except(nuevos);
            foreach (var item in eliminar)
            {
                _context.ProductoCategoria.Remove(entry.ProductoCategorias.Find(x => x.IdProductoCategoria == item));
            }
            await _context.SaveChangesAsync();
        }
    }
}
