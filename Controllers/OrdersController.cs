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

            var orderId = Guid.NewGuid();

            // Publicar evento de pedido creado
            await new KafkaService().PublishStatusAsync(orderId.ToString(), "CREADO");

            // Confirmar pedido
            new RabbitService().PublishOrderCreated(orderId.ToString());
            await new KafkaService().PublishStatusAsync(orderId.ToString(), "CONFIRMADO");

            // Simular envío
            await Task.Delay(2000);
            await new KafkaService().PublishStatusAsync(orderId.ToString(), "ENVIADO");

            return Ok(new { orderId, status = "ENVIADO" });
        }


        public record OrderDto(Guid ProductId, int Quantity);
        public record AvailabilityResponse(Guid productId, int stock, bool available);
    }
}
