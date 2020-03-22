using System;

namespace MyMusic.Domain.Events {

    public class PlayListHasBeenRenamed : Event {
        public string PlayListId { get; }
        public string NewPlayListName { get; }

        public PlayListHasBeenRenamed(string playListId, string newPlayListName) {
            PlayListId = playListId;
            NewPlayListName = newPlayListName;
        }

        protected bool Equals(PlayListHasBeenCreated other) {
            return PlayListId == other.PlayListId && NewPlayListName == other.PlayListName;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayListHasBeenCreated) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(PlayListId, NewPlayListName);
        }
    }
}