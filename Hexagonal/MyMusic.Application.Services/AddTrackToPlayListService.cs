using MyMusic.Model.PortsContracts;
using MyMusic.Model.PortsContracts.Http;

namespace MyMusic.Application.Services {
    
    public class AddTrackToPlayListService {
        
        private readonly TracksPersistencePort persistencePort;
        private readonly MusicHttpPort httpPort;

        public AddTrackToPlayListService(TracksPersistencePort persistencePort, MusicHttpPort httpPort) {
            this.persistencePort = persistencePort;
            this.httpPort = httpPort;
        }

        public void AddToPlayList(string trackId, string playlistId) {
            persistencePort.AddTrackToPlayList(trackId, playlistId);
            httpPort.NotifyTrackHasBeenAddedToPlayList(trackId, playlistId);
        }
        
    }
}