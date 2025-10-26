using Microsoft.AspNetCore.Mvc;
using WebApi_csharp.Models;
using WebApi_csharp.Repository;

namespace WebApi_csharp.Controllers
{
    [ApiController]
    [Route("inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IProductoRepository _productos;

        // Diccionario con stock por producto
        private static readonly Dictionary<Guid, int> _stock = new();

        public InventoryController(IProductoRepository productos)
        {
            _productos = productos;

            // Inicializa stock si aún no se creó
            if (!_stock.Any())
            {
                foreach (var p in _productos.GetAll())
                    _stock[p.Id] = new Random().Next(0, 20); // stock aleatorio para demo
            }
        }

        // Listar productos con su stock
        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = _productos.GetAll()
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Sale,
                    Stock = _stock.ContainsKey(p.Id) ? _stock[p.Id] : 0
                });

            return Ok(lista);
        }

        // Consultar disponibilidad de un producto
        [HttpGet("{productId}/availability")]
        public IActionResult GetAvailability(Guid productId, [FromQuery] int qty = 1)
        {
            var producto = _productos.GetById(productId);
            if (producto == null)
                return NotFound(new { message = "Producto no encontrado" });

            var stock = _stock.ContainsKey(productId) ? _stock[productId] : 0;
            bool available = stock >= qty;

            return Ok(new
            {
                producto.Id,
                producto.Name,
                producto.Sale,
                Stock = stock,
                Available = available
            });
        }
    }
}
