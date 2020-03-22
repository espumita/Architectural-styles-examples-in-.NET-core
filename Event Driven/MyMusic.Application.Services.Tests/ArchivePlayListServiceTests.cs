using FluentAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class ArchivePlayListServiceTests {
        
        private ArchivePlayListService archivePlayListService;
        private PlayListPersistencePort playListPersistence;
        private EventBusPort eventBusPort;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventBusPort = Substitute.For<EventBusPort>();
            archivePlayListService = new ArchivePlayListService(playListPersistence, eventBusPort);
        }

        [Test]
        public void archive_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .WithStatus(PlayListStatus.Active)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            
            var result = archivePlayListService.Execute(aPlaylistId);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, PlayListStatus.Archived);
            VerifyEventHasBeenRaised(new PlayListHasBeenArchived(aPlaylistId));
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, PlayListStatus status) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Status.Equals(status)
                           ));
        }
        private void VerifyEventHasBeenRaised(Event expectedEvent) {
            eventBusPort.Received()
                .Raise(Arg.Is<Event>(@event =>
                    @event.Equals(expectedEvent)));
        }
    }
}