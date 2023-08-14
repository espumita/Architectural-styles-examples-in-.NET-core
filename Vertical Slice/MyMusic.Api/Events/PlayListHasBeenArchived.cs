namespace MyMusic.Events {

    public class PlayListHasBeenArchived : Event {
        public string PlayListId { get;  }


        public PlayListHasBeenArchived(string playListId) {
            PlayListId = playListId;
        }

        protected bool Equals(PlayListHasBeenArchived other) {
            return PlayListId == other.PlayListId;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayListHasBeenArchived) obj);
        }

        public override int GetHashCode() {
            return (PlayListId != null ? PlayListId.GetHashCode() : 0);
        }
    }
}