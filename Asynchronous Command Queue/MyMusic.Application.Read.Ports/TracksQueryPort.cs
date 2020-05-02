using MyMusic.Application.Read.Model;

namespace MyMusic.Application.Read.Ports {

    public interface TracksQueryPort {
        Track GetTrack(string trackId);
    }
}