using FluentAssertions;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class ArchivePlayListServiceTests {
        
        private ArchivePlayListService archivePlayListService;
        private PlayListPersistencePort playListPersistence;
        private PlayListNotifierPort playListNotifier;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            playListNotifier = Substitute.For<PlayListNotifierPort>();
            archivePlayListService = new ArchivePlayListService(playListPersistence, playListNotifier);
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
            playListNotifier.Received().NotifyPlayListHasBeenArchived(aPlaylistId);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, PlayListStatus status) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Status.Equals(status)
                           ));
        }
    }
}