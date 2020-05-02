using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyMusic.Application.Commands;
using MyMusic.Application.Ports;
using MyMusic.CommandHandlerCreators;
using MyMusic.CommandProcessors;
using MyMusic.Domain.Events;
using MyMusic.EventConsumers;
using MyMusic.EventHandlerCreators;
using MyMusic.Infrastructure.Adapters;
using MyMusic.QueryCreators;

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
            AddCommandHandlersCreatorsToDependencyInjector(services);
            AddQueryCreatorsToDependencyInjector(services);
            AddEventHandlerCreatorsToDependencyInjector(services);

            var commandQueue = new CommandQueueInMemoryAdapter();
            RegisterPlayListCommandProcessorsInToDependencyInjector(services, commandQueue);
            RegisterTrackCommandProcessorsInToDependencyInjector(services, commandQueue);
            services.AddSingleton<CommandQueuePort>(commandQueue);
            
            var eventPublisher = new EventPublisherInMemoryAdapter();
            RegisterPlayListEventConsumerInToDependencyInjector(services, eventPublisher);
            RegisterTrackEventConsumerInToDependencyInjector(services, eventPublisher);
            services.AddSingleton<EventPublisherPort>(eventPublisher);
        }

        private static void AddCommandHandlersCreatorsToDependencyInjector(IServiceCollection services) {
            services.AddSingleton<PlayListCommandHandlerCreator>();
            services.AddSingleton<TracksCommandHandlerCreator>();
        }

        private static void AddQueryCreatorsToDependencyInjector(IServiceCollection services) {
            services.AddSingleton<PlayListQueryCreator>();
            services.AddSingleton<TracksQueryCreator>();
        }

        private static void AddEventHandlerCreatorsToDependencyInjector(IServiceCollection services) {
            services.AddSingleton<PlayListEventHandlerCreator>();
            services.AddSingleton<TrackEventHandlerCreator>();
        }

        private static void RegisterPlayListCommandProcessorsInToDependencyInjector(IServiceCollection services, CommandQueuePort commandQueue) {
            services.AddSingleton<PlayListCommandProcessor>();
            var playListCommandProcessor = services.BuildServiceProvider().GetService<PlayListCommandProcessor>();
            RegisterPlayListCommandProcessorsInTo(commandQueue, playListCommandProcessor);
        }

        private static void RegisterPlayListCommandProcessorsInTo(CommandQueuePort commandQueue, PlayListCommandProcessor playListCommandProcessor) {
            commandQueue.SetQueueSingleConsumer<CreatePLayList>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<RenamePlaylist>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<ChangePlayListImageUrl>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<ArchivePlayList>(playListCommandProcessor.Process);
        }
        
        private static void RegisterTrackCommandProcessorsInToDependencyInjector(IServiceCollection services, CommandQueuePort commandQueue) {
            services.AddSingleton<TrackCommandProcessor>();
            var trackCommandProcessor = services.BuildServiceProvider().GetService<TrackCommandProcessor>();
            RegisterTrackCommandProcessorsInTo(commandQueue, trackCommandProcessor);
        }

        private static void RegisterTrackCommandProcessorsInTo(CommandQueuePort commandQueue, TrackCommandProcessor trackCommandProcessor) {
        }
        
        private static void RegisterPlayListEventConsumerInToDependencyInjector(IServiceCollection services, EventPublisherPort eventPublisher) {
            services.AddSingleton<PlayListEventConsumer>();
            var playListEventConsumer = services.BuildServiceProvider().GetService<PlayListEventConsumer>();
            RegisterPlayListEventConsumersInTo(eventPublisher, playListEventConsumer);
        }

        private static void RegisterPlayListEventConsumersInTo(EventPublisherPort eventPublisher, PlayListEventConsumer playListEventConsumer) {
            eventPublisher.Register<PlayListHasBeenCreated>(playListEventConsumer.Consume);
            eventPublisher.Register<PlayListHasBeenRenamed>(playListEventConsumer.Consume);
            eventPublisher.Register<PlayListImageUrlHasChanged>(playListEventConsumer.Consume);
            eventPublisher.Register<PlayListHasBeenArchived>(playListEventConsumer.Consume);
        }

        private static void RegisterTrackEventConsumerInToDependencyInjector(IServiceCollection services, EventPublisherPort eventPublisher) {
            services.AddSingleton<TrackEventConsumer>();
            var trackEventConsumer = services.BuildServiceProvider().GetService<TrackEventConsumer>();
            RegisterTrackEventConsumersInTo(eventPublisher, trackEventConsumer);
        }

        private static void RegisterTrackEventConsumersInTo(EventPublisherPort eventPublisher, TrackEventConsumer trackEventConsumer) {
            eventPublisher.Register<TrackHasBeenAddedToPlayList>(trackEventConsumer.Consume);
            eventPublisher.Register<TrackHasBeenRemovedFromPlayList>(trackEventConsumer.Consume);
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