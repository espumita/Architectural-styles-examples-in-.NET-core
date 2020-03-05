using System.Collections.Generic;
using System.Linq;
using MyMusic.Domain;

namespace MyMusic.Responses {
    public class PlayListResponse : ResponseMapper {
        public string Id { get; }
        public string Name { get; }
        public List<TrackResponse> TrackList { get; }
        public string ImageUrl { get; }

        public PlayListResponse() { }

        private PlayListResponse(string id, string name, List<TrackResponse> trackList, string imageUrl) {
            Id = id;
            Name = name;
            TrackList = trackList;
            ImageUrl = imageUrl;
        }

        object ResponseMapper.From(object modelObject) {
            var playList = (PlayList) modelObject;
            var trackList = playList.TrackList.Select(TrackResponse.From).ToList();
            return new PlayListResponse(playList.Id, playList.Name, trackList, playList.ImageUrl);
        }

    }
}