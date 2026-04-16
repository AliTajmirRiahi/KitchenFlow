using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Presentation;
using Restaurant.Application;
using Restaurant.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Restaurant.Integration.Test.Presentation.Configs
{
    public class DependencyInjectionTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public DependencyInjectionTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public void Add_RestaurantDbContext_Should_Register_Expected_RestaurantDbContext()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();

            // Act
            var provider = scope.ServiceProvider;

            // Assert
            // مثال: فرض کنیم Infrastructure یک RestaurantDbContext اضافه می‌کنه
            var repo = provider.GetService<RestaurantDbContext>();
            Assert.NotNull(repo);
        }

        [Fact]
        public void Add_ApplicationService_Should_Register_Expected_Services()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();

            // Act
            var provider = scope.ServiceProvider;

            // Assert
            var service = provider.GetService<IOrderService>();
            Assert.NotNull(service);
        }

        [Fact]
        public void Add_Repository_Should_Register_Expected_Repositories()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();

            // Act
            var provider = scope.ServiceProvider;

            // Assert
            var repo = provider.GetService<IOrderRepository>();
            Assert.NotNull(repo);
        }
    }
}
