using Microsoft.Extensions.DependencyInjection;
using MyMusic.QueryCreators;

namespace MyMusic.Configuration {

    public static class QueriesConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton<PlayListQueryCreator>();
            services.AddSingleton<TracksQueryCreator>();
        }
    }
}