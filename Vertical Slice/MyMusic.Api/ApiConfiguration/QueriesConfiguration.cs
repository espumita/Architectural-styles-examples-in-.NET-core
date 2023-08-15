using Microsoft.Extensions.DependencyInjection;
using MyMusic.PlayLists;
using MyMusic.Tracks;

namespace MyMusic.ApiConfiguration {

    public static class QueriesConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton<PlayListQueryCreator>();
            services.AddSingleton<TracksQueryCreator>();
        }
    }
}