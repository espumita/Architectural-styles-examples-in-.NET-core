using MyMusic.PlayLists.Features.GetPlayListQuery;

namespace MyMusic.Tracks.Features {

    public interface TracksQuery {
        Track GetTrack(string trackId);
    }
}