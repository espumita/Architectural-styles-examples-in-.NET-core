namespace MyMusic.Application.Services.Errors {
    public class ServiceError {
        
        public static readonly CannotAddSameTrackTwice CannotAddSameTrackTwice = new CannotAddSameTrackTwice();
        public static readonly TrackIsNotInThe TrackIsNotInThe = new TrackIsNotInThe();
    }
}