using LanguageExt;
using MyMusic.Application.Queries.Errors;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;

namespace MyMusic.Application.Queries {
    public class GetTracksQuery {
        
        private readonly TracksQueryPort tracksQuery;

        public GetTracksQuery(TracksQueryPort tracksQuery) {
            this.tracksQuery = tracksQuery;
        }

        public Either<QueryError, Track> Get(string trackId) {
            return tracksQuery.GetTrack(trackId);
        }
    }
}