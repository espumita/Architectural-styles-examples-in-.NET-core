using FluentAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.Services.Tests {

    public class RenamePlayListServiceTests {
        
        private RenamePlayListService renamePlayListService;
        private PlayListPersistencePort playListPersistence;
        private EventPublisherPort eventPublisher;

        public RenamePlayListServiceTests() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            renamePlayListService = new RenamePlayListService(playListPersistence, eventPublisher);
        }

        [Fact]
        public void change_play_list_name() {
            var aPlaylistId = APlaylist.Id;
            var aPlaylistName = APlaylist.Name;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .WithName(aPlaylistName)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var anotherPlaylistName = APlaylist.AnotherName;
            
            var result = renamePlayListService.Execute(aPlaylistId, anotherPlaylistName);
            
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