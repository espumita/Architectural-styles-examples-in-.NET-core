using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Domain.Events;
using MyMusic.EventHandlers;
using MyMusic.Infrastructure.Adapters;
using MyMusic.QueryCreators;
using MyMusic.ServiceCreators;
using EventHandler = MyMusic.Application.SharedKernel.Model;

namespace MyMusic {
    public class Startup {
        
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddSingleton<PlayListServiceCreator>();
            services.AddSingleton<TracksServiceCreator>();
            services.AddSingleton<PlayListQueryCreator>();
            services.AddSingleton<TracksQueryCreator>();

            services.AddSingleton<PlayListHasBeenCreatedEventHandler>();

            var eventBus = new EventBusInMemoryAdapter();
            var playListHasBeenCreatedEventHandler = new PlayListHasBeenCreatedEventHandler();
            eventBus.Register<PlayListHasBeenCreated>(playListHasBeenCreatedEventHandler.Handle);
            services.AddSingleton(eventBus);
            
 
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
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