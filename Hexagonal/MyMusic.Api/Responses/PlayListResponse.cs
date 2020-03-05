using System.Collections.Generic;
using System.Linq;
using MyMusic.Domain;

namespace MyMusic.Responses {
    public class PlayListResponse : ResponseBuilder<PlayListResponse, PlayList> {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public List<TrackResponse> TrackList { get; private set; }
        public string ImageUrl { get; private set; }

        public PlayListResponse() { }
        

        public PlayListResponse BuildFrom(PlayList playList) {
            Id = playList.Id;
            Name = playList.Name;
            TrackList = playList.TrackList.Select(track => new TrackResponse().BuildFrom(track)).ToList();
            ImageUrl = playList.ImageUrl;
            return this;
        }
    }
}