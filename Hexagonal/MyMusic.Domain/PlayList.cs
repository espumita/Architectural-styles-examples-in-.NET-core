using System.Collections.Generic;
using System.Linq;

namespace MyMusic.Domain {
    public class PlayList {
        public string Id { get; }
        public string Name { get; private set; }
        public List<Track> TrackList { get; }
        public string ImageUrl { get; }

        public PlayList(string id, string name, List<Track> trackList, string imageUrl) {
            Id = id;
            Name = name;
            TrackList = trackList;
            ImageUrl = imageUrl;
        }

        public void Add(Track track) {
            TrackList.Add(track);
        }

        public void Remove(string trackId) {
            var trackToRemove = TrackList.First(track => track.Id.Equals(trackId));
            TrackList.Remove(trackToRemove);
        }

        public void Rename(string newPlayListName) {
            Name = newPlayListName;
        }
    }
}