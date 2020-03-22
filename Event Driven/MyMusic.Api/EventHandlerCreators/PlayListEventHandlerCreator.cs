
using MyMusic.Application.EventHandlers;
using MyMusic.Infrastructure.Adapters.Http;

namespace MyMusic.EventHandlerCreators {

    public class PlayListEventHandlerCreator {
        public PlayListHasBeenCreatedEventHandler PlayListHasBeenCreated() {
            var musicCloudApiHttpAdapter = new PlayListSpotifyApiAdapter();
            return new PlayListHasBeenCreatedEventHandler(musicCloudApiHttpAdapter);
        }
    }
}