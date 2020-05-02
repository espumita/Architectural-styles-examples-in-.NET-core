using Microsoft.Extensions.DependencyInjection;
using MyMusic.ServiceCreators;

namespace MyMusic.Configuration {

    public static class ServicesConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton<PlayListServiceCreator>();
            services.AddSingleton<TracksServiceCreator>();
        }
    }
}