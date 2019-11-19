using MyMusic.Model;
using MyMusic.Model.PortsContracts;
using MyMusic.Model.PortsContracts.Http;

namespace MyMusic.Application.Services {
    public class TracksService {
        private readonly TracksPersistencePort persistencePort;
        private readonly MusicHttpPort httpPort;

        public TracksService(TracksPersistencePort persistencePort) {
            this.persistencePort = persistencePort;
        }

        public TracksService(TracksPersistencePort persistencePort, MusicHttpPort httpPort) {
            this.persistencePort = persistencePort;
            this.httpPort = httpPort;
        }

        public void AddToPlayList(string trackId, string playlistId) {
            persistencePort.AddTrackToPlayList(trackId, playlistId);
            httpPort.NotifyTrackHasBeenAddedToPlayList(trackId, playlistId);
        }

        public void DeleteFromPlayList(string trackId, string playlistId) {
            persistencePort.DeleteTrackFromPlayList(trackId, playlistId);
            httpPort.NotifyTrackHasRemovedFromPlayList(trackId, playlistId);
        }

        public Track Get(string trackId) {
            return persistencePort.GetTrack(trackId);
        }
    }
}