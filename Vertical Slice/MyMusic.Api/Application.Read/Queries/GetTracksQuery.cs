using LanguageExt;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using MyMusic.Application.Read.Queries.Errors;

namespace MyMusic.Application.Read.Queries {
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