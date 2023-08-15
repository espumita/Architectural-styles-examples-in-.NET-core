using MyMusic.Tracks.Domain;

namespace MyMusic.Tracks.Features {
    public interface TracksPersistencePort {
        
        Track GetTrack(string trackId);
    }
}