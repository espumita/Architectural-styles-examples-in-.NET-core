using System.Collections.Generic;
using System.Linq;
using AwesomeAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Error;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.Services.Tests {

    public class AddTrackToPLayListServiceTests : ServiceTest {
        
        private AddTrackToPlayListService addTrackToPlayListService;
        private PlayListPersistencePort playListPersistence;
        private EventPublisherPort eventPublisher;

        public AddTrackToPLayListServiceTests() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            addTrackToPlayListService = new AddTrackToPlayListService(playListPersistence, eventPublisher);
        }
        
        [Fact]
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
            VerifyEventHasBeenRaised(new TrackHasBeenAddedToPlayList(aTrackId, aPlaylistId), eventPublisher);
        }

        [Fact]
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
            eventPublisher.DidNotReceive().Publish(Arg.Any<List<Event>>());
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string aTrackId) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.TrackList.Single().Id.Equals(aTrackId)
            ));
        }

    }
}