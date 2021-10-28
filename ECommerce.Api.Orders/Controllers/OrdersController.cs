using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    public class OrdersController : ControllerBase
    {
        public OrdersController(IOrdersProvider ordersProvider)
        {
            
        }
    }
}