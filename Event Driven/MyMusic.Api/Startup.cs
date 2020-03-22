using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyMusic.Domain.Events;
using MyMusic.EventConsumers;
using MyMusic.EventHandlerCreators;
using MyMusic.Infrastructure.Adapters;
using MyMusic.QueryCreators;
using MyMusic.ServiceCreators;

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
            services.AddSingleton<PlayListEventHandlerCreator>();

            services.AddSingleton<PlayListEventConsumer>();
            var playListHasBeenCreatedEventConsumer = services.BuildServiceProvider().GetService<PlayListEventConsumer>();

            var eventBus = new EventBusPortInMemoryAdapter();
            eventBus.Register<PlayListHasBeenCreated>(playListHasBeenCreatedEventConsumer.Consume);
            eventBus.Register<PlayListHasBeenRenamed>(playListHasBeenCreatedEventConsumer.Consume);
            eventBus.Register<PlayListImageUrlHasChanged>(playListHasBeenCreatedEventConsumer.Consume);
            eventBus.Register<PlayListHasBeenArchived>(playListHasBeenCreatedEventConsumer.Consume);
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