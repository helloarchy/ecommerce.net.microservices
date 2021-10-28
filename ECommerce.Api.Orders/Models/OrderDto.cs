﻿using System;
using System.Collections.Generic;

namespace ECommerce.Api.Orders.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}