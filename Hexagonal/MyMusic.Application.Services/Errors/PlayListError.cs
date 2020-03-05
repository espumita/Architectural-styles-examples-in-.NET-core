
namespace MyMusic.Application.Services.Errors {

    public class PlayListError {
        public static readonly CannotAddSameTrackTwice CannotAddSameTrackTwice = new CannotAddSameTrackTwice();
        public static readonly TrackIsNotInThePlayList TrackIsNotInThePlayList = new TrackIsNotInThePlayList();
    }
}