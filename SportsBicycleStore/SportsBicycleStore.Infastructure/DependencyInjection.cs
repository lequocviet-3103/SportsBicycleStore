using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Infastructure.Data;
using SportsBicycleStore.Infastructure.Repositories;
using SportsBicycleStore.Infastructure.Services;

namespace SportsBicycleStore.Infastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            //services.AddScoped<IUserRepository, UserRepository>();


            // Services
            //services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IMProductService, MProductService>();
            services.AddScoped<IMInspectionReportService, MInspectionReportService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IListingService, MlistingService>();
            services.AddScoped<IMOrderService, MOrderService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<IDisputeService, DisputeService>();

            return services;
        }

        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            // Services
            //services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
