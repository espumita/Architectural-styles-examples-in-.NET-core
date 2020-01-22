using MyMusic.Model;
using MyMusic.Model.PortsContracts;
using MyMusic.Model.PortsContracts.Http;

namespace MyMusic.Application.Services {
    public class GetTracksService {
        
        private readonly TracksPersistencePort persistencePort;
        private readonly MusicHttpPort httpPort;

        public GetTracksService(TracksPersistencePort persistencePort) {
            this.persistencePort = persistencePort;
        }

        public Track Get(string trackId) {
            return persistencePort.GetTrack(trackId);
        }
    }
}