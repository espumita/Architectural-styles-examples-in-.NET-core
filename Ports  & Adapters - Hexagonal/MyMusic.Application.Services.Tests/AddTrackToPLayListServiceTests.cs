using System.Linq;
using FluentAssertions;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Errors;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class AddTrackToPLayListServiceTests {
        
        private AddTrackToPlayListService addTrackToPlayListService;
        private PlayListPersistencePort playListPersistence;
        private TracksNotifierPort tracksNotifier;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            tracksNotifier = Substitute.For<TracksNotifierPort>();
            addTrackToPlayListService = new AddTrackToPlayListService(playListPersistence, tracksNotifier);
        }
        
        [Test]
        public void add_a_track_to_a_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);

            var result = addTrackToPlayListService.Execute(aTrackId, aPlaylistId);

            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, aTrackId);
            tracksNotifier.Received().NotifyTrackHasBeenAddedToPlayList(aTrackId, aPlaylistId);
        }

        [Test]
        public void do_not_add_a_track_twice() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .AddTrack(new TrackBuilder()
                    .WithId(aTrackId)
                    .Build())
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);

            var result = addTrackToPlayListService.Execute(aTrackId, aPlaylistId);

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().Be(DomainError.CannotAddSameTrackTwice));
            playListPersistence.DidNotReceive().Persist(Arg.Any<PlayList>());
            tracksNotifier.DidNotReceive().NotifyTrackHasBeenAddedToPlayList(Arg.Any<string>(), Arg.Any<string>());
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string aTrackId) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.TrackList.Single().Id.Equals(aTrackId)
            ));
        }
    }
}