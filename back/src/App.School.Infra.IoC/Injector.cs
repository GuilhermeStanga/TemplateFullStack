using App.School.Application.Students.Services;
using App.School.Domain.IUoW;
using App.School.Domain.Students.Interfaces;
using App.School.Infra.Data.UoW;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.School.Infra.IoC
{
    public static class Injector
    {
        public static void AddInjectorServices(this IServiceCollection services)
        {
            services.AddMediatR(
                config => config.AsScoped(),
                AppDomain.CurrentDomain.Load("App.School.Application")
            );
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<SchoolDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));    
            //options.UseNpgsql("Host=pgsql.local;Port=5432;Pooling=true;Database=ORBITA_CHALLENGE;User Id=postgres;Password=a!Bxg6ysb7S!;"));

            services.AddTransient<IStudentService, StudentService>();

            return services;
        }
    }
}
