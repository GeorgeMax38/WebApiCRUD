using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppSwager1.Data.Interfaces;
using WebAppSwager1.Models;
using WebAppSwager1.Dtos;

namespace WebAppSwager1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IApiRepository _apiRepository;

        public ProductosController(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _apiRepository.GetProductosAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _apiRepository.GetProductosByIdAsync(id);
            if (producto != null)
                return Ok(producto);

            return NotFound("Producto no encontrado");
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> Get(string nombre)
        {
            var producto = await _apiRepository.GetProductosByNombreAsync(nombre);
            if (producto != null)
                return Ok(producto);

            return NotFound("Producto no encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(DtoProductoCreate Dtoproducto)
        {
            var productoCreate = new Producto();

            productoCreate.Nombre = Dtoproducto.Nombre;
            productoCreate.Descripcion = Dtoproducto.Descripcion;
            productoCreate.Precio = Dtoproducto.Precio;

            productoCreate.FechaDeAlta = DateTime.Now;
            productoCreate.Activo = true;

            _apiRepository.Add(productoCreate);
            if (await _apiRepository.SaveAll())
                return Ok(productoCreate);

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DtoProductoUpdate Dtoproducto)
        {

            if (id != Dtoproducto.Id)
                return BadRequest("Los Id no coinciden");

            var datoupdate = await _apiRepository.GetProductosByIdAsync(Dtoproducto.Id);

            if (datoupdate == null)
                return BadRequest();

            //datoupdate.Nombre = producto.Nombre;
            datoupdate.Descripcion = Dtoproducto.Descripcion;
            datoupdate.Precio = Dtoproducto.Precio;
            datoupdate.FechaDeAlta = DateTime.Now;

            if (!await _apiRepository.SaveAll())
                return NoContent();

            return Ok(datoupdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _apiRepository.GetProductosByIdAsync(id);
            if(product == null)
                return NotFound("Producto no encontrado");

            _apiRepository.Delete(product);

            if (!await _apiRepository.SaveAll())
                return BadRequest("No se ´pudo eliminar el producto");

            return Ok("Producto borrado");
        }
    }
}
