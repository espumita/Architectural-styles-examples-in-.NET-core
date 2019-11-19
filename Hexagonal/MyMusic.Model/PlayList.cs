using System.Collections.Generic;

namespace MyMusic.Model {
    public class PlayList {
        public string Id { get; }
        public string Name { get; }
        
        public List<Track> TrackList { get; }

        public string ImageUrl { get; }
        public PlayList(string id, string name, List<Track> trackList, string imageUrl) {
            Id = id;
            Name = name;
            TrackList = trackList;
            ImageUrl = imageUrl;
        }
    }
}