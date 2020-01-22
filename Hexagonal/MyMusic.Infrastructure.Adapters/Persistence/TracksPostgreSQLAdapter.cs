using MyMusic.Model;
using MyMusic.Model.PortsContracts;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Infrastructure.Persistence {
    public class TracksPostgreSQLAdapter : TracksPersistencePort {
        public void AddTrackToPlayList(string trackId, string playlistId) {
            //This should update persistence
        }

        public void DeleteTrackFromPlayList(string trackId, string playlistId) {
            //This should update persistence
        }

        public Track GetTrack(string trackId) {
            //This should be read from persistence
            return new Track(trackId, "Mis Colegas", "Ska-P", 246600);
        }
    }
}