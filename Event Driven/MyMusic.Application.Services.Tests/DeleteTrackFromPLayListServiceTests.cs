using FluentAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class DeleteTrackFromPLayListServiceTests {
        
        private DeleteTrackFromPLayListService deleteTrackFromPLayListService;
        private PlayListPersistencePort playListPersistence;
        private EventBusPort eventBus;
        
        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventBus = Substitute.For<EventBusPort>();
            deleteTrackFromPLayListService = new DeleteTrackFromPLayListService(playListPersistence, eventBus);
        }
        
        [Test]
        public void remove_an_existing_track_from_a_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .AddTrack(new TrackBuilder()
                    .WithId(aTrackId)
                    .Build())
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);

            var result = deleteTrackFromPLayListService.Execute(aTrackId, aPlaylistId);

            result.IsRight.Should().BeTrue();
            VerifyAnEmptyPlayListHasBeenPersistedWith(aPlaylistId);
            VerifyEventHasBeenRaised(new TrackHasBeenDeletedFromPlayList(aTrackId, aPlaylistId));
        }

        [Test]
        public void do_not_remove_a_track_when_it_is_not_already_in_the_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);

            var result = deleteTrackFromPLayListService.Execute(aTrackId, aPlaylistId);

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().Be(ServiceError.TrackIsNotInThePlayList));
            playListPersistence.DidNotReceive().Persist(Arg.Any<PlayList>());
            eventBus.DidNotReceive().Raise(Arg.Any<Event>());
        }

        private void VerifyAnEmptyPlayListHasBeenPersistedWith(string aPlaylistId) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.TrackList.Count.Equals(0)
            ));
        }
        
        private void VerifyEventHasBeenRaised(Event expectedEvent) {
            eventBus.Received()
                .Raise(Arg.Is<Event>(@event =>
                    @event.Equals(expectedEvent)));
        }
        
    }
}