using LanguageExt;
using MyMusic.PlayLists.Features.GetPlayListQuery;
using MyMusic.Shared;

namespace MyMusic.Tracks.Features.GetTrack {
    public class GetTrackQuery {
        
        private readonly TracksQuery tracksQuery;

        public GetTrackQuery(TracksQuery tracksQuery) {
            this.tracksQuery = tracksQuery;
        }

        public Either<QueryError, Track> Get(string trackId) {
            return tracksQuery.GetTrack(trackId);
        }
    }
}