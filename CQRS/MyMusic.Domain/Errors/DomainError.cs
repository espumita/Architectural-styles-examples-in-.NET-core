namespace MyMusic.Domain.Errors {
    public class DomainError {
        public static readonly TrackIsNotInThePlayList TrackIsNotInThePlayList = new TrackIsNotInThePlayList();
        public static readonly CannotAddSameTrackTwice CannotAddSameTrackTwice = new CannotAddSameTrackTwice();
    }
}