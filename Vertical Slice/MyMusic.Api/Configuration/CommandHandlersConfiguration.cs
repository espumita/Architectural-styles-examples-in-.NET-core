using Microsoft.Extensions.DependencyInjection;
using MyMusic.CommandHandlerCreators;

namespace MyMusic.Configuration {

    public static class CommandHandlersConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton<PlayListCommandHandlerCreator>();
            services.AddSingleton<TracksCommandHandlerCreator>();
        }
    }
}