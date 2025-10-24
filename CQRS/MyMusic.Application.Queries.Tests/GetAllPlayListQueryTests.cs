using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyMusic.Application.Queries.Tests.builders;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using NSubstitute;
using Xunit;

namespace MyMusic.Application.Queries.Tests {

    public class GetAllPlayListQueryTests {
        private GetAllPlayListQuery getAllPlayListQuery;
        private PlayListQueryPort playListQuery;

        public GetAllPlayListQueryTests() {
            playListQuery = Substitute.For<PlayListQueryPort>();
            getAllPlayListQuery = new GetAllPlayListQuery(playListQuery);
        }

        [Fact]
        public void get_active_play_lists() {
            var aPlayListId = APlaylist.Id;
            var aPlayListName = APlaylist.Name;
            var aPlayListImageUrl = APlaylist.ImageUrl;
            var aPlayList = new PlayListBuilder()
                .WithId(aPlayListId)
                .WithName(aPlayListName)
                .WithImageUrl(aPlayListImageUrl)
                .WithStatus(PlayListStatus.Active)
                .AddTrack(new TrackBuilder()
                    .WithId(ATrack.Id)
                    .WithName(ATrack.Name)
                    .WithArtist(ATrack.Artist)
                    .WithDuration(ATrack.DurationInMs)
                    .Build())
                .Build();
            var anotherPlayListId = APlaylist.AnotherId;
            var anotherPlayList = new PlayListBuilder()
                .WithId(anotherPlayListId)
                .WithStatus(PlayListStatus.Archived)
                .Build();
            playListQuery.GetAllPlayList().Returns(new List<PlayList> {
                aPlayList, anotherPlayList
            });

            var result = getAllPlayListQuery.Execute();

            result.IsRight.Should().BeTrue();
            result.IfRight(listOfPlayLists => VerifyAreEquivalent(listOfPlayLists, aPlayList));
        }

        private static void VerifyAreEquivalent(ListOfPlayLists playListsList, PlayList expectedPlayList) {
            var playList = playListsList.Elements.Single();
            playList.Should().BeEquivalentTo(expectedPlayList);
        }
    }
}