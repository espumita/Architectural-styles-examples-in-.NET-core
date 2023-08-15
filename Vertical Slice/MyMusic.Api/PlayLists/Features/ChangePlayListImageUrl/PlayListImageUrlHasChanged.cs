using System;
using MyMusic.Shared;

namespace MyMusic.PlayLists.Features.ChangePlayListImageUrl {

    public class PlayListImageUrlHasChanged : Event {
        public string PlayListId { get; }
        public string ImageUrl { get; }

        public PlayListImageUrlHasChanged(string playListId, string imageUrl) {
            PlayListId = playListId;
            ImageUrl = imageUrl;
        }

        protected bool Equals(PlayListImageUrlHasChanged other) {
            return PlayListId == other.PlayListId && ImageUrl == other.ImageUrl;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayListImageUrlHasChanged) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(PlayListId, ImageUrl);
        }
    }
}