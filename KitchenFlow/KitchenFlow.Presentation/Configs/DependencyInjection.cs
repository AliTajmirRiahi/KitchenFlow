using Arta.Application.Core;
using Arta.Domain.Core.Commons;
using Arta.Domain.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Restaurant.Application;
using Restaurant.Domain.Order;
using Restaurant.Domain.Order.Mappers;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Repositories;

namespace Restaurant.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            // Add Applications
            services.Scan(scan => scan
                    .FromAssemblyOf<OrderService>() // 
                    .AddClasses(classes => classes.AssignableTo<IApplicationService>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Scan assembly that contains the concrete repository implementations
            services.Scan(scan => scan
                .FromAssemblyOf<OrderRepository>()   // Infrastructure assembly
                .AddClasses(classes => classes.AssignableTo<IRepositoryBase>()) 
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<MapperOrder>() 
                .AddClasses(classes => classes.Where(type =>
                    type.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IMapper<,>))))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }


        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Restaurant API",
                    Version = "v1",
                    Description = "This is the API documentation for the Restaurant system.",
                    Contact = new OpenApiContact
                    {
                        Name = "Support Team",
                        Email = "riahi.tajmir@gamil.com",
                        Url = new Uri("https://www.linkedin.com/in/ali-tajmir-riahi-532206170/")
                    },
                });
                //options.SwaggerDoc("v2", new OpenApiInfo { Title = "PixelForge V2", Version = "v2" });
            });
            return services;
        }
    }
}