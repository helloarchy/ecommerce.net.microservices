using System.Threading.Tasks;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await _ordersProvider.GetOrdersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var result = await _ordersProvider.GetOrderAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }

            return NotFound();
        }
    }
}