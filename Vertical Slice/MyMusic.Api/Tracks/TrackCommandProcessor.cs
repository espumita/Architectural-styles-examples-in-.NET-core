using LanguageExt;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.Shared.Commands.Successes;
using MyMusic.Shared.Websockets;

namespace MyMusic.Tracks {

    public class TrackCommandProcessor {
        private readonly TracksCommandHandlerCreator tracksCommandHandlerCreator;
        private readonly SignalRWebsocketAdapter signalRWebsocketAdapter;

        public TrackCommandProcessor(TracksCommandHandlerCreator tracksCommandHandlerCreator, SignalRWebsocketAdapter signalRWebsocketAdapter) {
            this.tracksCommandHandlerCreator = tracksCommandHandlerCreator;
            this.signalRWebsocketAdapter = signalRWebsocketAdapter;
        }
        
        public Either<DomainError, CommandResult> Process(Features.AddTrackToPLayList.AddTrackToPLayList command) {
            var commandHandler = tracksCommandHandlerCreator.CreateAddTrackToPlayListCommandHandler();
            return commandHandler.Handle(command);
        }

        public Either<DomainError, CommandResult> Process(Features.RemoveTrackFromPlayList.RemoveTrackFromPlayList command) {
            var service = tracksCommandHandlerCreator.CreateRemoveTrackFromPLayListCommandHandler();
            return service.Handle(command);
        }
    }
}