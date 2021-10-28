using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly ILogger<CustomersProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                // Jack
                _dbContext.Customers.Add(new Customer
                {
                    Id = 1,
                    Name = "Jack Riley",
                    Address = "12 Common Drive, Bergerenstein, Bergen, BE5 5EN"
                });

                // John
                _dbContext.Customers.Add(new Customer
                {
                    Id = 2,
                    Name = "John Long",
                    Address = "23 Fanwood Avenue, Claredon Court, Avesbury, AE5 9LU"
                });

                // Jane
                _dbContext.Customers.Add(new Customer
                {
                    Id = 3,
                    Name = "Jane Away",
                    Address = "34 Constance Place, Voyeurton, V11 6FR"
                });

                // Jill
                _dbContext.Customers.Add(new Customer
                {
                    Id = 4,
                    Name = "Jill Tailor",
                    Address = "45 Exmouth Street, Portsmouth, PR44 0MG"
                });

                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerDto> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var mappedDtos = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
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

        public async Task<(bool IsSuccess, CustomerDto Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    var mappedDto = _mapper.Map<Customer, CustomerDto>(customer);
                    return (true, mappedDto, null);
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