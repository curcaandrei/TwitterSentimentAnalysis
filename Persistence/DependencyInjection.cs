using Application.Interfaces;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services,ConfigurationManager configuration)
        {
            services.AddDbContext<MongoDbContext>(options =>
                options.UseSqlServer(
                    configuration["MongoUri"],
                    b => b.MigrationsAssembly(typeof(MongoDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<MongoDbContext>());
        }
    }
}