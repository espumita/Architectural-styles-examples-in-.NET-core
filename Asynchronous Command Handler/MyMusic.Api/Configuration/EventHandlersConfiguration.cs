using Microsoft.Extensions.DependencyInjection;
using MyMusic.EventHandlerCreators;

namespace MyMusic.Configuration {
    public static class EventHandlersConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton<PlayListEventHandlerCreator>();
            services.AddSingleton<TrackEventHandlerCreator>();
        }
    }
}