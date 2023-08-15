using System.Threading.Tasks;
using MyMusic.PlayList.Features;
using MyMusic.Tracks.Features;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.EventHandlers {

    public class TrackHasBeenAddedToPlayListEventHandlerTests {
        private TrackHasBeenAddedToPlayListEventHandler trackHasBeenAddedToPlayList;
        private TracksNotifierPort tracksNotifier;
        private WebsocketPort websocketPort;


        [SetUp]
        public void SetUp() {
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            websocketPort = Substitute.For<WebsocketPort>();
            trackHasBeenAddedToPlayList = new TrackHasBeenAddedToPlayListEventHandler(tracksNotifier, websocketPort);
        }

        [Test]
        public async Task notify_track_has_been_added_to_play_list_and_send_to_websocket() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var @event = new TrackHasBeenAddedToPlayList(aTrackId, aPlaylistId);

            await trackHasBeenAddedToPlayList.Handle(@event);
            
            tracksNotifier.Received().NotifyTrackHasBeenAddedToPlayList(aTrackId, aPlaylistId);
            await websocketPort.Received().PushMessageWithEventToAll(@event);
        }
    }
}