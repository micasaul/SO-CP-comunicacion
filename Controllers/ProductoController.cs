using Microsoft.AspNetCore.Mvc;
using WebApi_csharp.Models;
using WebApi_csharp.Services;

namespace WebApi_csharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {       
        //private readonly ILogger<ProductoController> _logger;
        private readonly IProductoService _service;

        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var producto = _service.GetById(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Producto producto)
        {
            _service.Create(producto);
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Producto producto)
        {
            producto.Id = id;
            var updated = _service.Update(producto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _service.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
