using MyMusic.Domain;

namespace MyMusic.Application.Write.Ports.Persistence {
    public interface TracksPersistencePort {
        
        Track GetTrack(string trackId);
    }
}