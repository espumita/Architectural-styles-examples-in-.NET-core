using System.Collections.Generic;
using Bogus;
using MyMusic.Domain;

namespace MyMusic.Application.Services.Tests.builders {
    public class PlayListBuilder {

        private string id;
        private string name;
        private PlayListStatus? status;
        private List<Track> trackList = new List<Track>();
        private string imageUrl;

        public PlayListBuilder WithId(string id) {
            this.id = id;
            return this;
        }

        public PlayListBuilder WithName(string name) {
            this.name = name;
            return this;
        }
        
        public PlayListBuilder WithStatus(PlayListStatus status) {
            this.status = status;
            return this;
        }

        public PlayListBuilder AddTrack(Track track) {
            trackList.Add(track);
            return this;
        }

        public PlayListBuilder WithImageUrl(string imageUrl) {
            this.imageUrl = imageUrl;
            return this;
        }

        public PlayList Build() {
            var faker = new Faker();
            return new PlayList(
                id: id ?? APlaylist.Id,
                name: name ?? APlaylist.Name,
                status: status ?? PlayListStatus.Active,
                trackList: trackList,
                imageUrl: imageUrl ?? APlaylist.ImageUrl
            );
        }
    }
}