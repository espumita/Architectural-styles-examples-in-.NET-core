using LanguageExt;

namespace MyMusic.Application.Services {

    public class PlayListError {
        public static readonly PlayListError CannotAddSameTrackTwice = new PlayListError();
        public static readonly PlayListError TrackIsNotInThePlayList = new PlayListError();
    }
}