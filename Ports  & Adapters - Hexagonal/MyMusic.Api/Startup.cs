using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyMusic.ServiceCreators;

namespace MyMusic {
    public class Startup {
        
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            ConfigureDependencyInjector(services);
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }
        
        private static void ConfigureDependencyInjector(IServiceCollection services) {
            AddServiceCreatorsToDependencyInjector(services);
        }
        
        private static void AddServiceCreatorsToDependencyInjector(IServiceCollection services) {
            services.AddSingleton<PlayListServiceCreator>();
            services.AddSingleton<TracksServiceCreator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}