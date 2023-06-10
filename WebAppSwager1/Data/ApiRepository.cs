using Microsoft.EntityFrameworkCore;
using WebAppSwager1.Data.Interfaces;
using WebAppSwager1.Models;

namespace WebAppSwager1.Data
{
    public class ApiRepository : IApiRepository
    {
        private readonly DataContext _context;
        public ApiRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Producto> GetProductosByIdAsync(int id)
        {
            var producto = await _context.Producto.FirstOrDefaultAsync(u => u.Id == id);
            return producto;
        }

        public async Task<Producto> GetProductosByNombreAsync(string Nombre)
        {
            var producto = await _context.Producto.FirstOrDefaultAsync(u => u.Nombre == Nombre);
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            var productos = await _context.Producto.ToListAsync();
            return productos;
        }

        public async Task<Usuario> GetUsuariosByIdAsync(int id)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }

        public async Task<Usuario> GetUsuariosByNombreAsync(string Nombre)
        {
            var usuarioName = await _context.Usuario.FirstOrDefaultAsync(u => u.Nombre == Nombre);
            return usuarioName;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var usuario = await _context.Usuario.ToListAsync();
            return usuario;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
