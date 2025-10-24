using MyMusic.Application.Ports;
using MyMusic.Configuration;
using MyMusic.Infrastructure.Adapters;

namespace MyMusic {
    public class Startup {
        
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            ConfigureDependencyInjector(services);
            services.AddOpenApi();
        }

        private static void ConfigureDependencyInjector(IServiceCollection services) {
            QueriesConfiguration.Configure(services);
            ConfigureEvents(services);
            ServicesConfiguration.Configure(services);
        }
        
        private static void ConfigureEvents(IServiceCollection services) {
            EventHandlersConfiguration.Configure(services);
            var eventPublisher = new EventPublisherInMemoryAdapter();
            EventConsumersConfiguration.Configure(services, eventPublisher);
            services.AddSingleton<EventPublisherPort>(eventPublisher);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
            });
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapOpenApi();
                endpoints.MapControllers();
            });
        }
    }
}