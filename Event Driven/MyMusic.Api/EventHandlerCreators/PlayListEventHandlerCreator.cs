
using MyMusic.Application.EventHandlers;
using MyMusic.Domain.Events;
using MyMusic.Infrastructure.Adapters.Http;

namespace MyMusic.EventHandlerCreators {

    public class PlayListEventHandlerCreator {
        public PlayListHasBeenCreatedEventHandler PlayListHasBeenCreated() {
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new PlayListHasBeenCreatedEventHandler(musicCloudApiHttpAdapter);
        }

        public PlayListHasBeenArchivedEventHandler PlayListHasBeenArchived() {
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new PlayListHasBeenArchivedEventHandler(musicCloudApiHttpAdapter);
        }

        public PlayListHasBeenRenamedEventHandler PlayListHasBeenRenamed() {
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new PlayListHasBeenRenamedEventHandler(musicCloudApiHttpAdapter);
        }

        public PlayListHasImageUrlHasChangedEventHandler PlayListImageUrlHasChanged() {
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new PlayListHasImageUrlHasChangedEventHandler(musicCloudApiHttpAdapter);
        }
    }
}