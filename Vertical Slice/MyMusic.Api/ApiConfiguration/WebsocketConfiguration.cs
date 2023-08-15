using Microsoft.Extensions.DependencyInjection;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.ApiConfiguration {
    public static class WebsocketConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton(new SignalRWebsocket());
        }
    }
}