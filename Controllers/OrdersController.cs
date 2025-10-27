using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebApi_csharp.Models;
using WebApi_csharp.Services;

namespace WebApi_csharp.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly HttpClient _http;

        public OrdersController(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient("inventory");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDto order)
        {
            var res = await _http.GetFromJsonAsync<AvailabilityResponse>(
                $"inventory/{order.ProductId}/availability?qty={order.Quantity}");

            if (res == null || !res.available)
                return Conflict(new { message = "Producto sin stock" });

            // Si hay stock, confirmamos el pedido
            var orderId = Guid.NewGuid();

            // Publicar mensaje en RabbitMQ
            new RabbitService().PublishOrderCreated(orderId.ToString());

            // Respuesta final
            return Ok(new { orderId, status = "CONFIRMED" });
        }


        public record OrderDto(Guid ProductId, int Quantity);
        public record AvailabilityResponse(Guid productId, int stock, bool available);
    }
}
