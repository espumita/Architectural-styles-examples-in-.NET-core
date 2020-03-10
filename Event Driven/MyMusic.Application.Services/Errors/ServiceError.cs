using MyMusic.Application.SharedKernel.Model;

namespace MyMusic.Application.Services.Errors {
    public class ServiceError : Error {
        
        public static readonly CannotAddSameTrackTwice CannotAddSameTrackTwice = new CannotAddSameTrackTwice();
        public static readonly TrackIsNotInThe TrackIsNotInThe = new TrackIsNotInThe();
    }
}