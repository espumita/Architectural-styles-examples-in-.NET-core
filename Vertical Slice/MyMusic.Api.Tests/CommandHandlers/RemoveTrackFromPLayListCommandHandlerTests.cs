using System.Collections.Generic;
using FluentAssertions;
using MyMusic.Api.Tests.CommandHandlers.builders;
using MyMusic.Application.Write.CommandHandlers;
using MyMusic.Application.Write.Commands;
using MyMusic.Application.Write.Ports;
using MyMusic.Application.Write.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Error;
using MyMusic.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.CommandHandlers {

    public class RemoveTrackFromPLayListCommandHandlerTests : CommandHandlerTest {
        
        private RemoveTrackFromPLayListCommandHandler removeTrackFromPLayListCommandHandler;
        private PlayListPersistencePort playListPersistence;
        private EventPublisherPort eventPublisher;
        
        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            removeTrackFromPLayListCommandHandler = new RemoveTrackFromPLayListCommandHandler(playListPersistence, eventPublisher);
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
            var command = new RemoveTrackFromPlayList(aTrackId, aPlaylistId);

            var result = removeTrackFromPLayListCommandHandler.Handle(command);

            result.IsRight.Should().BeTrue();
            VerifyAnEmptyPlayListHasBeenPersistedWith(aPlaylistId);
            VerifyEventHasBeenRaised(new TrackHasBeenRemovedFromPlayList(aTrackId, aPlaylistId), eventPublisher);
        }

        [Test]
        public void do_not_remove_a_track_when_it_is_not_already_in_the_play_list() {
            var aTrackId = ATrack.Id;
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var command = new RemoveTrackFromPlayList(aTrackId, aPlaylistId);
            
            var result = removeTrackFromPLayListCommandHandler.Handle(command);

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().Be(DomainError.TrackIsNotInThePlayList));
            playListPersistence.DidNotReceive().Persist(Arg.Any<PlayList>());
            eventPublisher.DidNotReceive().Publish(Arg.Any<List<Event>>());
        }

        private void VerifyAnEmptyPlayListHasBeenPersistedWith(string aPlaylistId) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.TrackList.Count.Equals(0)
            ));
        }
        
    }
}