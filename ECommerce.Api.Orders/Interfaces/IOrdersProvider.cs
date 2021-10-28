﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderDto> Orders, string ErrorMessage)> GetOrdersAsync();
        Task<(bool IsSuccess, OrderDto Order, string ErrorMessage)> GetOrderAsync(int id);
    }
}