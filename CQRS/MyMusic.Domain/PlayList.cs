using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using MyMusic.Domain.Errors;

namespace MyMusic.Domain {
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

        public static PlayList Create(string id, string name) {
            return new PlayList(id, name, PlayListStatus.Active, new List<Track>(), null);
        }
        
        public void Add(Track track) {
            TrackList.Add(track);
        }

        public Option<DomainError> Remove(string trackId) {
            var trackToRemove = TrackList.FirstOrDefault(track => track.Id.Equals(trackId));
            if (trackToRemove == null) return DomainError.TrackIsNotInThePlayList;
            TrackList.Remove(trackToRemove);
            return Option<DomainError>.None;
        }

        public void Rename(string newPlayListName) {
            Name = newPlayListName;
        }

        public void Archive() {
            Status = PlayListStatus.Archived;
        }

        public void AddImageUrl(string aNewImageUrL) {
            ImageUrl = aNewImageUrL;
        }
    }
}