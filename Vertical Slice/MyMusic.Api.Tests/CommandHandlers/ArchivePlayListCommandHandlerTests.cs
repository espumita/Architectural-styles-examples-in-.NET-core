using FluentAssertions;
using MyMusic.Api.Tests.CommandHandlers.builders;
using MyMusic.PlayList;
using MyMusic.PlayList.Domain;
using MyMusic.PlayList.Features;
using MyMusic.PlayList.Features.ArchivePlayList;
using MyMusic.Shared.Ports;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.CommandHandlers {

    public class ArchivePlayListCommandHandlerTests : CommandHandlerTest {
        
        private ArchivePlayListCommandHandler archivePlayListCommandHandler;
        private PlayListPersistencePort playListPersistence;
        private EventPublisherPort eventPublisher;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            archivePlayListCommandHandler = new ArchivePlayListCommandHandler(playListPersistence, eventPublisher);
        }

        [Test]
        public void archive_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .WithStatus(PlayListStatus.Active)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var command = new ArchivePlayList(aPlaylistId);

            var result = archivePlayListCommandHandler.Handle(command);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, PlayListStatus.Archived);
            VerifyEventHasBeenRaised(new PlayListHasBeenArchived(aPlaylistId), eventPublisher);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, PlayListStatus status) {
            playListPersistence.Received().Persist(Arg.Is<PlayList.Domain.PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Status.Equals(status)
                           ));
        }
    }
}