using System;

namespace MyMusic.Domain.Events {

    public class TrackHasBeenDeletedFromPlayList : Event {
        public string TrackId { get; }
        public string PlayListId { get; }

        public TrackHasBeenDeletedFromPlayList(string trackId, string playListId) {
            TrackId = trackId;
            PlayListId = playListId;
        }

        protected bool Equals(TrackHasBeenDeletedFromPlayList other) {
            return TrackId == other.TrackId && PlayListId == other.PlayListId;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TrackHasBeenDeletedFromPlayList) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(TrackId, PlayListId);
        }
    }
}