using MyMusic.CommandHandlerCreators;

namespace MyMusic.CommandProcessors {

    public class TrackCommandProcessor {
        private readonly TracksCommandHandlerCreator tracksCommandHandlerCreator;

        public TrackCommandProcessor(TracksCommandHandlerCreator tracksCommandHandlerCreator) {
            this.tracksCommandHandlerCreator = tracksCommandHandlerCreator;
        }
    }
}