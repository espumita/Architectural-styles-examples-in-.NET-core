using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyMusic.PlayLists.Features;
using MyMusic.PlayLists.Features.GetAllPlayListQuery;
using MyMusic.PlayLists.Features.GetPlayListQuery;
using NSubstitute;
using NUnit.Framework;

namespace MyMusic.Api.Tests.PlayLists.GetAllPlayListQuery {

    public class GetAllPlayListQueryTests {
        private MyMusic.PlayLists.Features.GetAllPlayListQuery.GetAllPlayListQuery getAllPlayListQuery;
        private PlayListQueryPort playListQuery;

        [SetUp]
        public void SetUp() {
            playListQuery = Substitute.For<PlayListQueryPort>();
            getAllPlayListQuery = new MyMusic.PlayLists.Features.GetAllPlayListQuery.GetAllPlayListQuery(playListQuery);
        }

        [Test]
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