using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Tests.builders;
using MyMusic.Domain;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Services.Tests {

    public class GetAllPlayListServiceTests {
        private GetAllPlayListService getAllPlayListService;
        private PlayListPersistencePort playListPersistencePort;

        [SetUp]
        public void SetUp() {
            playListPersistencePort = Substitute.For<PlayListPersistencePort>();
            getAllPlayListService = new GetAllPlayListService(playListPersistencePort);
        }

        [Test]
        public void get_active_play_lists() {
            var aPlayListId = APlaylist.Id;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlayListId)
                .WithStatus(PlayListStatus.Active)
                .Build();
            var anotherPlayListId = APlaylist.AnotherId;
            var anotherPlayList = new PlayListBuilder()
                .WithId(anotherPlayListId)
                .WithStatus(PlayListStatus.Archived)
                .Build();
            playListPersistencePort.GetAllPlayList().Returns(new List<PlayList> {
                aPlayList, anotherPlayList
            });

            var result = getAllPlayListService.Execute();

            result.IsRight.Should().BeTrue();
            result.Match(
                Right: playListsList => playListsList.Elements.Single().Id.Should().Be(aPlayListId),
                Left: error => null
            );
        }
        
    }
}