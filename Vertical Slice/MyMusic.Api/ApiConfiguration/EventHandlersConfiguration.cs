using Microsoft.Extensions.DependencyInjection;
using MyMusic.PlayLists;
using MyMusic.Tracks;

namespace MyMusic.ApiConfiguration {
    public static class EventHandlersConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton<PlayListEventHandlerCreator>();
            services.AddSingleton<TrackEventHandlerCreator>();
        }
    }
}