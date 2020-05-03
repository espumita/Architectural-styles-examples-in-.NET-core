using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.CommandHandlerCreators;
using MyMusic.Domain.Error;

namespace MyMusic.CommandProcessors {

    public class TrackCommandProcessor : CommandProcessor {
        private readonly TracksCommandHandlerCreator tracksCommandHandlerCreator;
        private readonly SignalRWebsocketAdapter signalRWebsocketAdapter;

        public TrackCommandProcessor(TracksCommandHandlerCreator tracksCommandHandlerCreator, SignalRWebsocketAdapter signalRWebsocketAdapter) {
            this.tracksCommandHandlerCreator = tracksCommandHandlerCreator;
            this.signalRWebsocketAdapter = signalRWebsocketAdapter;
        }
        
        public Either<DomainError, CommandResult> Process(AddTrackToPLayList command) {
            var commandHandler = tracksCommandHandlerCreator.CreateAddTrackToPlayListCommandHandler();
            return commandHandler.Handle(command);
        }

        public Either<DomainError, CommandResult> Process(RemoveTrackFromPlayList command) {
            var service = tracksCommandHandlerCreator.CreateRemoveTrackFromPLayListCommandHandler();
            return service.Handle(command);
        }
    }
}