using MyMusic.Configuration;

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
            ServicesConfiguration.Configure(services);
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