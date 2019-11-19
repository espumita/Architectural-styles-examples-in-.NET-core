using MyMusic.Model;
using MyMusic.Model.PortsContracts;

namespace MyMusic.Infrastructure.Persistence {
    public class TracksDatabaseAdapter : TracksPersistencePort {
        public void AddTrackToPlayList(string trackId, string playlistId) {
            //This should update persistence
        }

        public void DeleteTrackFromPlayList(string trackId, string playlistId) {
            //This should update persistence
        }

        public Track GetTrack(string trackId) {
            return new Track(trackId);
        }
    }
}