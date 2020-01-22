using MyMusic.Model.PortsContracts;
using MyMusic.Model.PortsContracts.Http;

namespace MyMusic.Application.Services {
    
    public class DeleteTrackFromPLayListService {
        
        private readonly TracksPersistencePort persistencePort;
        private readonly MusicHttpPort httpPort;

        public DeleteTrackFromPLayListService(TracksPersistencePort persistencePort, MusicHttpPort httpPort) {
            this.persistencePort = persistencePort;
            this.httpPort = httpPort;
        }

        public void DeleteFromPlayList(string trackId, string playlistId) {
            persistencePort.DeleteTrackFromPlayList(trackId, playlistId);
            httpPort.NotifyTrackHasRemovedFromPlayList(trackId, playlistId);
        }
        
    }
}