using MyMusic.PlayList.Features.GetPlayListQuery;

namespace MyMusic.Tracks.Features {

    public interface TracksQueryPort {
        Track GetTrack(string trackId);
    }
}