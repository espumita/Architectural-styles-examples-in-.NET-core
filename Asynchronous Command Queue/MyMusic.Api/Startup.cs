using Microsoft.AspNetCore.Http.Connections;
using MyMusic.Application.Ports;
using MyMusic.Configuration;
using MyMusic.Infrastructure.Adapters;
using MyMusic.Websockets;

namespace MyMusic {
    public class Startup {
        
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddCors();
            services.AddControllers();
            ConfigureDependencyInjector(services);
            services.AddOpenApi();
            services.AddSignalR();
        }

        private static void ConfigureDependencyInjector(IServiceCollection services) {
            QueriesConfiguration.Configure(services);
            WebsocketConfiguration.Configure(services);
            ConfigureEvents(services);
            ConfigureCommands(services);
        }

        private static void ConfigureEvents(IServiceCollection services) {
            EventHandlersConfiguration.Configure(services);
            var eventPublisher = new EventPublisherInMemoryAdapter();
            EventConsumersConfiguration.Configure(services, eventPublisher);
            services.AddSingleton<EventPublisherPort>(eventPublisher);
        }

        private static void ConfigureCommands(IServiceCollection services) {
            CommandHandlersConfiguration.Configure(services);
            var commandQueue = ConfigureCommandQueue(services);
            CommandProcessorsConfiguration.Configure(services, commandQueue);
            services.AddSingleton<CommandQueuePort>(commandQueue);
        }

        private static AsynchronousCommandQueueInMemoryAdapter ConfigureCommandQueue(IServiceCollection services) {
            services.AddSingleton<WebsocketErrorHandlerDecorator>();
            var errorHandlerDecorator = services.BuildServiceProvider().GetService<WebsocketErrorHandlerDecorator>();
            return new AsynchronousCommandQueueInMemoryAdapter(errorHandlerDecorator);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.WithOrigins(
                    "http://localhost:8081", "*")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
            });
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapOpenApi();
                endpoints.MapControllers();
                endpoints.MapHub<SignalRWebsocketAdapter>("/MyMusicHub", options => {
                    options.Transports = HttpTransportType.WebSockets;
                });
            });
            
        }
    }
}