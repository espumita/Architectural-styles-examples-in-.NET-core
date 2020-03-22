using FluentAssertions;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class AddImageUrlToPlayListServiceTests {
        
        private AddImageUrlToPlayListService addImageUrlToPlayListService;
        private PlayListPersistencePort playListPersistence;
        private EventBusPort eventBus;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventBus = Substitute.For<EventBusPort>();
            addImageUrlToPlayListService = new AddImageUrlToPlayListService(playListPersistence, eventBus);
        }

        [Test]
        public void add_an_image_url_to_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var anImageUrl = APlaylist.AnotherImageUrl;
            
            var result = addImageUrlToPlayListService.Execute(aPlaylistId, anImageUrl);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, anImageUrl);
            VerifyEventHasBeenRaised(new PlayListImageUrlHasChanged(aPlaylistId, anImageUrl));
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string anImageUrl) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.ImageUrl.Equals(anImageUrl)
            ));
        }
        
        private void VerifyEventHasBeenRaised(Event expectedEvent) {
            eventBus.Received()
                .Raise(Arg.Is<Event>(@event =>
                    @event.Equals(expectedEvent)));
        }
    }
}