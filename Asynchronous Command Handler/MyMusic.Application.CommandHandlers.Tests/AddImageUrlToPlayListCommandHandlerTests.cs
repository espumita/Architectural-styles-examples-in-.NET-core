using FluentAssertions;
using MyMusic.Application.CommandHandlers.Tests.builders;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Events;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.CommandHandlers.Tests {

    public class AddImageUrlToPlayListCommandHandlerTests : CommandHandlerTest {
        
        private AddImageUrlToPlayListCommandHandler addImageUrlToPlayListCommandHandler;
        private PlayListPersistencePort playListPersistence;
        private EventPublisherPort eventPublisher;

        [SetUp]
        public void SetUp() {
            playListPersistence = Substitute.For<PlayListPersistencePort>();
            eventPublisher = Substitute.For<EventPublisherPort>();
            addImageUrlToPlayListCommandHandler = new AddImageUrlToPlayListCommandHandler(playListPersistence, eventPublisher);
        }

        [Test]
        public void add_an_image_url_to_a_play_list() {
            var aPlaylistId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlaylistId)
                .Build();
            playListPersistence.GetPlayList(aPlaylistId).Returns(aPlayList);
            var anImageUrl = APlaylist.AnotherImageUrl;
            
            var result = addImageUrlToPlayListCommandHandler.Execute(aPlaylistId, anImageUrl);
            
            result.IsRight.Should().BeTrue();
            VerifyPlayListHasBeenPersistedWith(aPlaylistId, anImageUrl);
            VerifyEventHasBeenRaised(new PlayListImageUrlHasChanged(aPlaylistId, anImageUrl), eventPublisher);
        }

        private void VerifyPlayListHasBeenPersistedWith(string aPlaylistId, string anImageUrl) {
            playListPersistence.Received().Persist(Arg.Is<PlayList>(playlist =>
                playlist.Id.Equals(aPlaylistId)
                && playlist.ImageUrl.Equals(anImageUrl)
            ));
        }
        
    }
}