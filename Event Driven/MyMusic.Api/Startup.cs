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
            ConfigureDependencyInjector(services);
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        private static void ConfigureDependencyInjector(IServiceCollection services) {
            AddServiceCreatorsToDependencyInjector(services);
            AddQueryCreatorsToDependencyInjector(services);
            AddEventHandlerCreatorsToDependencyInjector(services);
            var eventBus = new EventBusInMemoryAdapter();
            RegisterPlayListEventConsumerInToDependencyInjector(services, eventBus);
            RegisterTrackEventConsumerInToDependencyInjector(services, eventBus);
            services.AddSingleton<EventBusPort>(eventBus);
        }

        private static void AddServiceCreatorsToDependencyInjector(IServiceCollection services) {
            services.AddSingleton<PlayListServiceCreator>();
            services.AddSingleton<TracksServiceCreator>();
        }

        private static void AddQueryCreatorsToDependencyInjector(IServiceCollection services) {
            services.AddSingleton<PlayListQueryCreator>();
            services.AddSingleton<TracksQueryCreator>();
        }

        private static void AddEventHandlerCreatorsToDependencyInjector(IServiceCollection services) {
            services.AddSingleton<PlayListEventHandlerCreator>();
            services.AddSingleton<TrackEventHandlerCreator>();
        }

        private static void RegisterPlayListEventConsumerInToDependencyInjector(IServiceCollection services, EventBusPort eventBus) {
            services.AddSingleton<PlayListEventConsumer>();
            var playListEventConsumer = services.BuildServiceProvider().GetService<PlayListEventConsumer>();
            RegisterPlayListEventConsumersInTo(eventBus, playListEventConsumer);
        }

        private static void RegisterPlayListEventConsumersInTo(EventBusPort eventBus, PlayListEventConsumer playListEventConsumer) {
            eventBus.Register<PlayListHasBeenCreated>(playListEventConsumer.Consume);
            eventBus.Register<PlayListHasBeenRenamed>(playListEventConsumer.Consume);
            eventBus.Register<PlayListImageUrlHasChanged>(playListEventConsumer.Consume);
            eventBus.Register<PlayListHasBeenArchived>(playListEventConsumer.Consume);
        }

        private static void RegisterTrackEventConsumerInToDependencyInjector(IServiceCollection services, EventBusInMemoryAdapter eventBus) {
            services.AddSingleton<TrackEventConsumer>();
            var trackEventConsumer = services.BuildServiceProvider().GetService<TrackEventConsumer>();
            RegisterTrackEventConsumersInTo(eventBus, trackEventConsumer);
        }

        private static void RegisterTrackEventConsumersInTo(EventBusInMemoryAdapter eventBus, TrackEventConsumer trackEventConsumer) {
            eventBus.Register<TrackHasBeenAddedToPlayList>(trackEventConsumer.Consume);
            eventBus.Register<TrackHasBeenRemovedFromPlayList>(trackEventConsumer.Consume);
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