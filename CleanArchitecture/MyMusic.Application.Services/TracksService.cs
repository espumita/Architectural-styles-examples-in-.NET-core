using MyMusic.Model;
using MyMusic.Model.PortsContracts;

namespace MyMusic.Application.Services {
    public class TracksService {
        private readonly TracksPersistencePort persistencePort;

        public TracksService(TracksPersistencePort persistencePort) {
            this.persistencePort = persistencePort;
        }

        public void AddToPlayList(string trackId, string playlistId) {
            persistencePort.AddTrackToPlayList(trackId, playlistId);
        }

        public void DeleteFromPlayList(string trackId, string playlistId) {
            persistencePort.DeleteTrackFromPlayList(trackId, playlistId);
        }

        public Track Get(string trackId) {
            return persistencePort.GetTrack(trackId);
        }
    }
}