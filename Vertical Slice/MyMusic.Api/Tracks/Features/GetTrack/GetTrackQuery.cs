using LanguageExt;
using MyMusic.PlayList.Features.GetPlayListQuery;
using MyMusic.Shared.Queries.Errors;

namespace MyMusic.Tracks.Features.GetTrack {
    public class GetTrackQuery {
        
        private readonly TracksQueryPort tracksQuery;

        public GetTrackQuery(TracksQueryPort tracksQuery) {
            this.tracksQuery = tracksQuery;
        }

        public Either<QueryError, Track> Get(string trackId) {
            return tracksQuery.GetTrack(trackId);
        }
    }
}