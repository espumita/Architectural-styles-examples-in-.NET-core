using System;

namespace MyMusic.Domain.Events {

    public class TrackHasBeenRemovedFromPlayList : Event {
        public string TrackId { get; }
        public string PlayListId { get; }

        public TrackHasBeenRemovedFromPlayList(string trackId, string playListId) {
            TrackId = trackId;
            PlayListId = playListId;
        }

        protected bool Equals(TrackHasBeenRemovedFromPlayList other) {
            return TrackId == other.TrackId && PlayListId == other.PlayListId;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TrackHasBeenRemovedFromPlayList) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(TrackId, PlayListId);
        }
    }
}