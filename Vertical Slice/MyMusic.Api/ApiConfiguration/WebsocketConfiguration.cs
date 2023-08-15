using Microsoft.Extensions.DependencyInjection;
using MyMusic.Shared.Websockets;

namespace MyMusic.ApiConfiguration {
    public static class WebsocketConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton(new SignalRWebsocketAdapter());
        }
    }
}