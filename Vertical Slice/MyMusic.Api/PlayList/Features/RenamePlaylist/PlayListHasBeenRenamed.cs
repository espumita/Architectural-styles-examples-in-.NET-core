using System;
using MyMusic.Shared;

namespace MyMusic.PlayList.Features.RenamePlaylist {

    public class PlayListHasBeenRenamed : Event {
        public string PlayListId { get; }
        public string NewPlayListName { get; }

        public PlayListHasBeenRenamed(string playListId, string newPlayListName) {
            PlayListId = playListId;
            NewPlayListName = newPlayListName;
        }

        protected bool Equals(PlayListHasBeenRenamed other) {
            return PlayListId == other.PlayListId && NewPlayListName == other.NewPlayListName;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayListHasBeenRenamed) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(PlayListId, NewPlayListName);
        }
    }
}