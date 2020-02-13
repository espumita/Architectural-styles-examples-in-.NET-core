using System.Collections.Generic;
using System.Linq;
using MyMusic.Domain;

namespace MyMusic.Responses {
    public class PlayListResponse {
        public string Id { get; }
        public string Name { get; }
        public List<TrackResponse> TrackList { get; }
        public string ImageUrl { get; }

        private PlayListResponse(string id, string name, List<TrackResponse> trackList, string imageUrl) {
            Id = id;
            Name = name;
            TrackList = trackList;
            ImageUrl = imageUrl;
        }

        public static PlayListResponse From(PlayList playList) {
            var trackList = playList.TrackList.Select(TrackResponse.From).ToList();
            return new PlayListResponse(playList.Id, playList.Name, trackList, playList.ImageUrl);
        }
    }
}