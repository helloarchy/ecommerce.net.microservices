using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any())
            {
                // Keyboard
                _dbContext.Products.Add(new Product
                {
                    Id = 1,
                    Name = "Keyboard",
                    Price = 20,
                    Inventory = 100
                });
                
                // Mouse
                _dbContext.Products.Add(new Product
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 5,
                    Inventory = 200
                });
                
                // Monitor
                _dbContext.Products.Add(new Product
                {
                    Id = 3,
                    Name = "Monitor",
                    Price = 350,
                    Inventory = 86
                });
                
                // PC
                _dbContext.Products.Add(new Product
                {
                    Id = 4,
                    Name = "PC",
                    Price = 1250,
                    Inventory = 28
                });

                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var mappedDtos = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
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