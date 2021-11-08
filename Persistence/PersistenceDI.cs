using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.v1;

namespace Persistence
{
    public static class PersistenceDI
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<ProductContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyConnection"), b => b.MigrationsAssembly(typeof(ProductContext).Assembly.FullName)));
            services.AddDbContext<TweetContext>(options => options.UseSqlite("Data Source=MyProducts.db"));
            // services.AddScoped<IApplicationContext, ProductContext>();
            // register implementations related to repository/generic implementation
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ITwitterRepository, TweetRepository>();
        }
    }
}