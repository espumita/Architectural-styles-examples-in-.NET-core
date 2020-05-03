using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Configuration;
using MyMusic.Infrastructure.Adapters;

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
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
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
            var commandQueue = new AsynchronousCommandQueueInMemoryAdapter();
            CommandProcessorsConfiguration.Configure(services, commandQueue);
            services.AddSingleton<CommandQueuePort>(commandQueue);
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
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRWebsocketAdapter>("/MyMusicHub", options => {
                    options.Transports = HttpTransportType.WebSockets;
                });
            });
            
        }
    }
}