using System.Diagnostics.CodeAnalysis;
using Application.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.TwitterExternalAPI;

namespace Persistence
{
    [ExcludeFromCodeCoverage]
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<ITweetsRepository, TweetRepository>();
            serviceCollection.AddScoped<ITwitterHelper, TwitterHelper>();
            serviceCollection.AddScoped<IExternalTweetRepository, ExternalTwitterRepository>();
            serviceCollection.AddScoped<IMlRepository, MlRepository>();
            serviceCollection.AddScoped<IRequestTweetRepository, RequestTweetRepository>();
            return serviceCollection;
        }
    }
}