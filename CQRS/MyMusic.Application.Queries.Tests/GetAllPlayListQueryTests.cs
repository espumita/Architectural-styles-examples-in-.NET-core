using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyMusic.Application.Read.Ports;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Application.Queries.Tests {

    public class GetAllPlayListQueryTests {
        private GetAllPlayListQuery getAllPlayListQuery;
        private PlayListQueryPort playListQueryPort;

        [SetUp]
        public void SetUp() {
            playListQueryPort = Substitute.For<PlayListQueryPort>();
            getAllPlayListQuery = new GetAllPlayListQuery(playListQueryPort);
        }
/*
        [Test]
        public void get_active_play_lists() {
            var aPlayListId = APlaylist.Id;
            new PlayList()
            var aPlayList = new PlayListBuilder()
                .WithId(aPlayListId)
                .WithStatus(PlayListStatus.Active)
                .Build();
            var anotherPlayListId = APlaylist.AnotherId;
            var anotherPlayList = new PlayListBuilder()
                .WithId(anotherPlayListId)
                .WithStatus(PlayListStatus.Archived)
                .Build();
            playListQueryPort.GetAllPlayList().Returns(new List<PlayList> {
                aPlayList, anotherPlayList
            });

            var result = getAllPlayListService.Execute();

            result.IsRight.Should().BeTrue();
            result.Match(
                Right: playListsList => playListsList.Elements.Single().Id.Should().Be(aPlayListId),
                Left: error => null
            );
        }
        */
    }
}