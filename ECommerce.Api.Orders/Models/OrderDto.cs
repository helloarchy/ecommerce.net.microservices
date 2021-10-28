﻿using System;
using ECommerce.Api.Orders.Db;

namespace ECommerce.Api.Orders.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public OrderItem[] Items { get; set; }
    }
}