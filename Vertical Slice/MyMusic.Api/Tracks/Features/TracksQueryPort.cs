using MyMusic.PlayLists.Features.GetPlayListQuery;

namespace MyMusic.Tracks.Features {

    public interface TracksQueryPort {
        Track GetTrack(string trackId);
    }
}