using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.CommandHandlerCreators;
using MyMusic.Domain.Error;
using MyMusic.Websockets;

namespace MyMusic.CommandProcessors {

    public class TrackCommandProcessor {
        private readonly TracksCommandHandlerCreator tracksCommandHandlerCreator;
        private readonly SignalRWebsocketAdapter signalRWebsocketAdapter;

        public TrackCommandProcessor(TracksCommandHandlerCreator tracksCommandHandlerCreator, SignalRWebsocketAdapter signalRWebsocketAdapter) {
            this.tracksCommandHandlerCreator = tracksCommandHandlerCreator;
            this.signalRWebsocketAdapter = signalRWebsocketAdapter;
        }
        
        public Either<DomainError, CommandResult> Process(AddTrackToPlayList command) {
            var commandHandler = tracksCommandHandlerCreator.CreateAddTrackToPlayListCommandHandler();
            return commandHandler.Handle(command);
        }

        public Either<DomainError, CommandResult> Process(RemoveTrackFromPlayList command) {
            var service = tracksCommandHandlerCreator.CreateRemoveTrackFromPlayListCommandHandler();
            return service.Handle(command);
        }
    }
}