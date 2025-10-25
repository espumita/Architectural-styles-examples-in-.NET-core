using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePlayList;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Domain.Error;
using MyMusic.Tracks.Features.AddTrackToPlayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.Shared.Domain {
    public class PlayList {
        
        public string Id { get; }
        public string Name { get; private set; }
        public PlayListStatus Status { get; private set; }
        public List<Track> TrackList { get; }
        public string ImageUrl { get; private set; }
        private List<Event> events { get;  set; }
        
        public PlayList(string id, string name, PlayListStatus status, List<Track> trackList, string imageUrl) {
            Id = id;
            Name = name;
            Status = status;
            TrackList = trackList;
            ImageUrl = imageUrl;
            events = new List<Event>();
        }

        public static PlayList Create(string id, string name) {
            var playList = new PlayList(id, name, PlayListStatus.Active, new List<Track>(), null);
            playList.Create();
            return playList;
        }

        private void Create() {
            events.Add(new PlayListHasBeenCreated(Id, Name));
        }

        public Option<DomainError> Add(Track track) {
            var trackToAddAlreadyInPlayList = TrackList.FirstOrDefault(x => x.Id.Equals(track.Id));
            if (trackToAddAlreadyInPlayList != null) return DomainError.CannotAddSameTrackTwice;
            TrackList.Add(track);
            events.Add(new TrackHasBeenAddedToPlayList(track.Id, Id));
            return Option<DomainError>.None;
        }

        public Option<DomainError> Remove(string trackId) {
            var trackToRemove = TrackList.FirstOrDefault(track => track.Id.Equals(trackId));
            if (trackToRemove == null) return DomainError.TrackIsNotInThePlayList;
            TrackList.Remove(trackToRemove);
            events.Add(new TrackHasBeenRemovedFromPlayList(trackId, Id));
            return Option<DomainError>.None;
        }

        public void Rename(string newPlayListName) {
            Name = newPlayListName;
            events.Add(new PlayListHasBeenRenamed(Id, Name));
        }

        public void Archive() {
            Status = PlayListStatus.Archived;
            events.Add(new PlayListHasBeenArchived(Id));
        }

        public void AddImageUrl(string aNewImageUrL) {
            ImageUrl = aNewImageUrL;
            events.Add(new PlayListImageUrlHasChanged(Id, ImageUrl));
        }

        public List<Event> Events() {
            return events;
        }
    }
}