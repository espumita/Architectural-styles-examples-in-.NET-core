using Microsoft.Extensions.DependencyInjection;

namespace MyMusic.Configuration {
    public static class WebsocketConfiguration {
        public static void Configure(IServiceCollection services) {
            services.AddSingleton(new SignalRWebsocketAdapter());
        }
    }
}