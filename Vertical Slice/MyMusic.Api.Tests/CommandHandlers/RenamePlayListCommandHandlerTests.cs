using FluentAssertions;
using MyMusic.Api.Tests.CommandHandlers.builders;
using MyMusic.PlayList.Features;
using MyMusic.PlayList.Features.RenamePlaylist;
using MyMusic.Shared.Ports;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.CommandHandlers {

    public class RenamePlayListCommandHandlerTests {
        
        private RenamePlayListCommandHandler renamePlayListCommandHandler;
        private PlayListPersistencePort playListPersistence;
        private EventPublisherPort eventPublisher;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
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
            var command = new RenamePlaylist(aPlaylistId, anotherPlaylistName);

            var result = renamePlayListCommandHandler.Handle(command);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, anotherPlaylistName);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string anotherPlaylistName) {
            playListPersistence.Received().Persist(Arg.Is<PlayList.Domain.PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.Name.Equals(anotherPlaylistName)
            ));
        }
        

    }
}