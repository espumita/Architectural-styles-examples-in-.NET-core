using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyMusic.Application.Ports;
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
            services.AddSingleton<TrackEventHandlerCreator>();

            services.AddSingleton<PlayListEventConsumer>();
            var playListEventConsumer = services.BuildServiceProvider().GetService<PlayListEventConsumer>();

            var eventBus = new EventBusPortInMemoryAdapter();
            eventBus.Register<PlayListHasBeenCreated>(playListEventConsumer.Consume);
            eventBus.Register<PlayListHasBeenRenamed>(playListEventConsumer.Consume);
            eventBus.Register<PlayListImageUrlHasChanged>(playListEventConsumer.Consume);
            eventBus.Register<PlayListHasBeenArchived>(playListEventConsumer.Consume);
            
            services.AddSingleton<TrackEventConsumer>();
            var trackEventConsumer = services.BuildServiceProvider().GetService<TrackEventConsumer>();
            eventBus.Register<TrackHasBeenAddedToPlayList>(trackEventConsumer.Consume);
            eventBus.Register<TrackHasBeenRemovedFromPlayList>(trackEventConsumer.Consume);
            
            services.AddSingleton<EventBusPort>(eventBus);
            
 
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