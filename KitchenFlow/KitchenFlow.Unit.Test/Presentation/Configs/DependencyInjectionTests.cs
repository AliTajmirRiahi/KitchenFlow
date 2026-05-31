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
using Arta.Application.Core;
using Castle.Core.Configuration;

namespace Restaurant.Unit.Test.Presentation.Configs
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void Add_RestaurantDbContext_Should_Register_Expected_RestaurantDbContext()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddInfrastructure(configuration);

            var provider = services.BuildServiceProvider();

            // Assert
            var repo = provider.GetService<RestaurantDbContext>();
            Assert.NotNull(repo);
        }

        [Fact]
        public void Add_ApplicationService_Should_Register_Expected_Services()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddInfrastructure(configuration);
            services.AddApplications();
            services.AddRepositories();
            services.AddMappers();

            var provider = services.BuildServiceProvider();

            // Assert
            var service = provider.GetService<IApplicationService>();
            Assert.NotNull(service);
        }

        [Fact]
        public void Add_Repository_Should_Register_Expected_Repositories()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddInfrastructure(configuration);
            services.AddRepositories();

            var provider = services.BuildServiceProvider();

            // Assert
            var service = provider.GetService<IOrderRepository>();
            Assert.NotNull(service);
        }
    }
}
