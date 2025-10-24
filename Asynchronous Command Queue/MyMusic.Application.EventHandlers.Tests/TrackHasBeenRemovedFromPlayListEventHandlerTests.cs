using System.Threading.Tasks;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Websockets;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.EventHandlers.Tests {

    public class TrackHasBeenRemovedFromPlayListEventHandlerTests {
        private TrackHasBeenRemovedFromPlayListEventHandler trackHasBeenRemovedFromPlayList;
        private TracksNotifierPort tracksNotifier;
        private WebsocketPort websocketPort;


        public TrackHasBeenRemovedFromPlayListEventHandlerTests() {
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            websocketPort = Substitute.For<WebsocketPort>();
            trackHasBeenRemovedFromPlayList = new TrackHasBeenRemovedFromPlayListEventHandler(tracksNotifier, websocketPort);
        }

        [Fact]
        public async Task notify_track_has_removed_added_to_play_list_and_send_to_websocket() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var @event = new TrackHasBeenRemovedFromPlayList(aTrackId, aPlaylistId);

            await trackHasBeenRemovedFromPlayList.Handle(@event);
            
            tracksNotifier.Received().NotifyTrackHasRemovedFromPlayList(aTrackId, aPlaylistId);
            await websocketPort.Received().PushMessageWithEventToAll(@event);
        }
    }
}