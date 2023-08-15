using LanguageExt;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.Shared;
using MyMusic.Shared.Infrastructure;

namespace MyMusic.Tracks {

    public class TrackCommandProcessor {
        private readonly TracksCommandHandlerCreator tracksCommandHandlerCreator;
        private readonly SignalRWebsocket signalRWebsocket;

        public TrackCommandProcessor(TracksCommandHandlerCreator tracksCommandHandlerCreator, SignalRWebsocket signalRWebsocket) {
            this.tracksCommandHandlerCreator = tracksCommandHandlerCreator;
            this.signalRWebsocket = signalRWebsocket;
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