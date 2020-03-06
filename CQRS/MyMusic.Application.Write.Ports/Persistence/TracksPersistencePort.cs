using MyMusic.Domain;

namespace MyMusic.Application.Ports.Persistence {
    public interface TracksPersistencePort {
        
        Track GetTrack(string trackId);
    }
}