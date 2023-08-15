using Microsoft.Extensions.DependencyInjection;
using MyMusic.PlayLists;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePLayList;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Ports;
using MyMusic.Tracks;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.ApiConfiguration {

    public static class EventConsumersConfiguration {
        public static void Configure(IServiceCollection services, EventPublisherPort eventPublisher) {
            RegisterPlayListEventConsumerInToDependencyInjector(services, eventPublisher);
            RegisterTrackEventConsumerInToDependencyInjector(services, eventPublisher);
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
    }
}