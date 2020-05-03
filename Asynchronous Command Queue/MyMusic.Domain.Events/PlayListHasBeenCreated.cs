using System;

namespace MyMusic.Domain.Events {
    public class PlayListHasBeenCreated : Event {
        public string PlayListId { get; }
        public string PlayListName { get; }

        public PlayListHasBeenCreated() { }

        public PlayListHasBeenCreated(string playListId, string playListName) {
            PlayListId = playListId;
            PlayListName = playListName;
        }

        protected bool Equals(PlayListHasBeenCreated other) {
            return PlayListId == other.PlayListId && PlayListName == other.PlayListName;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayListHasBeenCreated) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(PlayListId, PlayListName);
        }
    }
}