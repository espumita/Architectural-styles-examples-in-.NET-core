using Microsoft.Extensions.DependencyInjection;
using MyMusic.PlayLists;
using MyMusic.Tracks;

namespace MyMusic.ApiConfiguration {

    public static class CommandHandlersConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton<PlayListCommandHandlerCreator>();
            services.AddSingleton<TracksCommandHandlerCreator>();
        }
    }
}