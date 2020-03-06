using LanguageExt;
using MyMusic.Application.Read.Model;
using MyMusic.Application.Read.Ports;
using MyMusic.Application.SharedKernel.Model;

namespace MyMusic.Application.Queries {
    public class GetTracksQuery {
        
        private readonly TracksQueryPort tracksQueryPort;

        public GetTracksQuery(TracksQueryPort tracksQueryPort) {
            this.tracksQueryPort = tracksQueryPort;
        }

        public Either<Error, Track> Get(string trackId) {
            return tracksQueryPort.GetTrack(trackId);
        }
    }
}