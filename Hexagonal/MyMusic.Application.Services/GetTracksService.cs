using MyMusic.Model;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Application.Services {
    public class GetTracksService {
        
        private readonly TracksPersistencePort tracksPersistence;

        public GetTracksService(TracksPersistencePort tracksPersistence) {
            this.tracksPersistence = tracksPersistence;
        }

        public Track Get(string trackId) {
            return tracksPersistence.GetTrack(trackId);
        }
    }
}