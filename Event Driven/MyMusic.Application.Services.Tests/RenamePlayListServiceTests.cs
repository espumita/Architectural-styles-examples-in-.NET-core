using FluentAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class RenamePlayListServiceTests {
        
        private RenamePlayListService renamePlayListService;
        private PlayListPersistencePort playListPersistence;
        private EventBusPort eventBus;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventBus = Substitute.For<EventBusPort>();
            renamePlayListService = new RenamePlayListService(playListPersistence, eventBus);
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
        
        private void VerifyEventHasBeenRaised(Event expectedEvent) {
            eventBus.Received()
                .Raise(Arg.Is<Event>(@event =>
                    @event.Equals(expectedEvent)));
        }
    }
}