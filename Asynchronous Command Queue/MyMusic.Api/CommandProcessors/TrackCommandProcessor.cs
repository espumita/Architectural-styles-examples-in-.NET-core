using MyMusic.Application.Commands;
using MyMusic.CommandHandlerCreators;

namespace MyMusic.CommandProcessors {

    public class TrackCommandProcessor {
        private readonly TracksCommandHandlerCreator tracksCommandHandlerCreator;

        public TrackCommandProcessor(TracksCommandHandlerCreator tracksCommandHandlerCreator) {
            this.tracksCommandHandlerCreator = tracksCommandHandlerCreator;
        }
        
        public void Process(AddTrackToPLayList command) {
            var commandHandler = tracksCommandHandlerCreator.CreateAddTrackToPlayListCommandHandler();
            var result = commandHandler.Handle(command);
        }
        public void Process(RemoveTrackFromPlayList command) {
            var service = tracksCommandHandlerCreator.CreateRemoveTrackFromPLayListCommandHandler();
            var result = service.Handle(command);
        }
    }
}