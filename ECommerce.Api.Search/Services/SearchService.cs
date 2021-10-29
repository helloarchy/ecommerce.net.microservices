using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;

        public SearchService(IOrdersService ordersService, IProductsService productsService)
        {
            _ordersService = ordersService;
            _productsService = productsService;
        }
        
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrdersAsync(customerId);
            var productsResult = await _productsService.GetProductsAsync();
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        var product = productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId);
                        item.ProductName = product?.Name;
                    }
                }
                
                
                var result = new
                {
                    Orders = ordersResult.Orders
                };

                return (true, result);
            }

            return (false, null);
        }
    }
}