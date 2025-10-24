using System.Threading.Tasks;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.EventHandlers.Tests {

    public class TrackHasBeenAddedToPlayListEventHandlerTests {
        private TrackHasBeenAddedToPlayListEventHandler trackHasBeenAddedToPlayList;
        private TracksNotifierPort tracksNotifier;
        private WebsocketPort websocketPort;


        public TrackHasBeenAddedToPlayListEventHandlerTests() {
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            websocketPort = Substitute.For<WebsocketPort>();
            trackHasBeenAddedToPlayList = new TrackHasBeenAddedToPlayListEventHandler(tracksNotifier, websocketPort);
        }

        [Fact]
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