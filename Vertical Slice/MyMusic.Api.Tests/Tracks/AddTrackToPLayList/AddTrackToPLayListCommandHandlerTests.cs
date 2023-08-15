using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyMusic.Api.Tests.Shared;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Domain;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.PlayLists.Features;
using MyMusic.Shared;
using MyMusic.Shared.Ports;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.Tracks.AddTrackToPLayList {

    public class AddTrackToPLayListCommandHandlerTests : CommandHandlerTest {
        
        private AddTrackToPlayListCommandHandler addTrackToPlayListCommandHandler;
        private PlayListPersistencePort playListPersistence;
        private EventPublisherPort eventPublisher;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            addTrackToPlayListCommandHandler = new AddTrackToPlayListCommandHandler(playListPersistence, eventPublisher);
        }
        
        [Test]
        public void add_a_track_to_a_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var command = new MyMusic.Tracks.Features.AddTrackToPLayList.AddTrackToPLayList(aTrackId, aPlaylistId);

            var result = addTrackToPlayListCommandHandler.Handle(command);

            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, aTrackId);
            VerifyEventHasBeenRaised(new TrackHasBeenAddedToPlayList(aTrackId, aPlaylistId), eventPublisher);
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
            var command = new MyMusic.Tracks.Features.AddTrackToPLayList.AddTrackToPLayList(aTrackId, aPlaylistId);

            var result = addTrackToPlayListCommandHandler.Handle(command);

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