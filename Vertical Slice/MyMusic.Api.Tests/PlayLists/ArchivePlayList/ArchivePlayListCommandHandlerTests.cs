using FluentAssertions;
using MyMusic.Api.Tests.Shared;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.Shared.Domain;
using MyMusic.Shared.Infrastructure;
using MyMusic.Shared.Persistence;
using NSubstitute;
using Xunit;

namespace MyMusic.Api.Tests.PlayLists.ArchivePlayList {

    public class ArchivePlayListCommandHandlerTests : CommandHandlerTest {
        
        private ArchivePlayListCommandHandler archivePlayListCommandHandler;
        private PlayListPersistence playListPersistence;
        private EventPublisher eventPublisher;

        public ArchivePlayListCommandHandlerTests() {
            playListPersistence = Substitute.For<PlayListPersistence>();
            eventPublisher = Substitute.For<EventPublisher>();
            archivePlayListCommandHandler = new ArchivePlayListCommandHandler(playListPersistence, eventPublisher);
        }

        [Fact]
        public void archive_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .WithStatus(PlayListStatus.Active)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var command = new MyMusic.PlayLists.Features.ArchivePlayList.ArchivePlayList(aPlaylistId);

            var result = archivePlayListCommandHandler.Handle(command);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, PlayListStatus.Archived);
            VerifyEventHasBeenRaised(new PlayListHasBeenArchived(aPlaylistId), eventPublisher);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, PlayListStatus status) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Status.Equals(status)
                           ));
        }
    }
}