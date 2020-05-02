using System.Collections.Generic;

namespace MyMusic.Application.Read.Model {

    public class PlayList {
        public string Id { get; }
        public string Name { get; private set; }
        public PlayListStatus Status { get; private set; }
        public List<Track> TrackList { get; }
        public string ImageUrl { get; private set; }
        
        public PlayList(string id, string name, PlayListStatus status, List<Track> trackList, string imageUrl) {
            Id = id;
            Name = name;
            Status = status;
            TrackList = trackList;
            ImageUrl = imageUrl;
        }
    }
}