using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;

namespace MyMusic.Infrastructure.Adapters.Persistence {
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