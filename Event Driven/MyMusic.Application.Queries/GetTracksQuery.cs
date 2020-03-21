using LanguageExt;
using MyMusic.Application.Queries.Errors;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;

namespace MyMusic.Application.Queries {
    public class GetTracksQuery {
        
        private readonly TracksQueryPort tracksQueryPort;

        public GetTracksQuery(TracksQueryPort tracksQueryPort) {
            this.tracksQueryPort = tracksQueryPort;
        }

        public Either<QueryError, Track> Get(string trackId) {
            return tracksQueryPort.GetTrack(trackId);
        }
    }
}