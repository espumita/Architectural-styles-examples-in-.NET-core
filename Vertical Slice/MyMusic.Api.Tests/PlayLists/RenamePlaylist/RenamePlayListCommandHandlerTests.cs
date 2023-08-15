using FluentAssertions;
using MyMusic.Api.Tests.Shared.builders;
using MyMusic.PlayLists.Domain;
using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.PlayLists.RenamePlaylist {

    public class RenamePlayListCommandHandlerTests {
        
        private RenamePlayListCommandHandler renamePlayListCommandHandler;
        private PlayListPersistence playListPersistence;
        private EventPublisher eventPublisher;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistence>();
            eventPublisher = Substitute.For<EventPublisher>();
            renamePlayListCommandHandler = new RenamePlayListCommandHandler(playListPersistence, eventPublisher);
        }

        [Test]
        public void change_play_list_name() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .WithName(aPlaylistName)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var anotherPlaylistName = APlaylist.AnotherName;
            var command = new MyMusic.PlayLists.Features.RenamePlaylist.RenamePlaylist(aPlaylistId, anotherPlaylistName);

            var result = renamePlayListCommandHandler.Handle(command);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, anotherPlaylistName);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string anotherPlaylistName) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Name.Equals(anotherPlaylistName)
            ));
        }
        

    }
}