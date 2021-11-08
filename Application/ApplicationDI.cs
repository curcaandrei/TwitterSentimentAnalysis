using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Application
{
    public static class ApplicationDI
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}