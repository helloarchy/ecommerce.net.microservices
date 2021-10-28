using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Orders.Any())
            {
                // Keyboard for Jane
                _dbContext.Orders.Add(new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = new DateTime(),
                    Total = 10,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 1,
                            UnitPrice = 10
                        }
                    }
                });

                // Mouse and Monitor for John
                _dbContext.Orders.Add(new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = new DateTime(),
                    Total = 270,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Id = 2,
                            OrderId = 2,
                            ProductId = 2,
                            Quantity = 1,
                            UnitPrice = 20
                        },
                        new OrderItem
                        {
                            Id = 3,
                            OrderId = 3,
                            ProductId = 3,
                            Quantity = 1,
                            UnitPrice = 250
                        }
                    }
                });

                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderDto> Orders, string ErrorMessage)> GetOrdersAsync(
            int customerId)
        {
            try
            {
                var orders = await _dbContext.Orders.Where(order => order.CustomerId == customerId).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var mappedDtos = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
                    return (true, mappedDtos, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}