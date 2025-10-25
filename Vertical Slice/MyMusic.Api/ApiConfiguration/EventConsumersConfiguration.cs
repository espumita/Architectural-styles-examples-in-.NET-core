using Microsoft.Extensions.DependencyInjection;
using MyMusic.PlayLists;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePlayList;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Infrastructure;
using MyMusic.Tracks;
using MyMusic.Tracks.Features.AddTrackToPlayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.ApiConfiguration {

    public static class EventConsumersConfiguration {
        public static void Configure(IServiceCollection services, EventPublisher eventPublisher) {
            RegisterPlayListEventConsumerInToDependencyInjector(services, eventPublisher);
            RegisterTrackEventConsumerInToDependencyInjector(services, eventPublisher);
        }
        
        private static void RegisterPlayListEventConsumerInToDependencyInjector(IServiceCollection services, EventPublisher eventPublisher) {
            services.AddSingleton<PlayListEventConsumer>();
            var playListEventConsumer = services.BuildServiceProvider().GetService<PlayListEventConsumer>();
            RegisterPlayListEventConsumersInTo(eventPublisher, playListEventConsumer);
        }

        private static void RegisterPlayListEventConsumersInTo(EventPublisher eventPublisher, PlayListEventConsumer playListEventConsumer) {
            eventPublisher.Register<PlayListHasBeenCreated>(playListEventConsumer.Consume);
            eventPublisher.Register<PlayListHasBeenRenamed>(playListEventConsumer.Consume);
            eventPublisher.Register<PlayListImageUrlHasChanged>(playListEventConsumer.Consume);
            eventPublisher.Register<PlayListHasBeenArchived>(playListEventConsumer.Consume);
        }

        private static void RegisterTrackEventConsumerInToDependencyInjector(IServiceCollection services, EventPublisher eventPublisher) {
            services.AddSingleton<TrackEventConsumer>();
            var trackEventConsumer = services.BuildServiceProvider().GetService<TrackEventConsumer>();
            RegisterTrackEventConsumersInTo(eventPublisher, trackEventConsumer);
        }

        private static void RegisterTrackEventConsumersInTo(EventPublisher eventPublisher, TrackEventConsumer trackEventConsumer) {
            eventPublisher.Register<TrackHasBeenAddedToPlayList>(trackEventConsumer.Consume);
            eventPublisher.Register<TrackHasBeenRemovedFromPlayList>(trackEventConsumer.Consume);
        }
    }
}