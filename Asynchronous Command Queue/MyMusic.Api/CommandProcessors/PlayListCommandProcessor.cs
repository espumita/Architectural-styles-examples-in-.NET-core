using LanguageExt;
using MyMusic.Application.Commands;
using MyMusic.Application.Commands.Successes;
using MyMusic.CommandHandlerCreators;
using MyMusic.Domain.Error;

namespace MyMusic.CommandProcessors {

    public class PlayListCommandProcessor {
        private readonly PlayListCommandHandlerCreator playListCommandHandlerCreator;

        public PlayListCommandProcessor(PlayListCommandHandlerCreator playListCommandHandlerCreator) {
            this.playListCommandHandlerCreator = playListCommandHandlerCreator;
        }

        public Either<DomainError, CommandResult> Process(CreatePlayList command) {
            var commandHandler = playListCommandHandlerCreator.CreateCreatePlayListCommandHandler();
            return commandHandler.Handle(command);
        }
        
        public Either<DomainError, CommandResult> Process(RenamePlaylist command) {
            var commandHandler = playListCommandHandlerCreator.CreateRenamePlayListCommandHandler();
            return commandHandler.Handle(command);
        }
        
        public Either<DomainError, CommandResult> Process(ChangePlayListImageUrl command) {
            var commandHandler = playListCommandHandlerCreator.CreateAddImageUrlPlayListCommandHandler();
            return commandHandler.Handle(command);
        }
        public Either<DomainError, CommandResult> Process(ArchivePlayList command) {
            var commandHandler = playListCommandHandlerCreator.CreateArchivePlayListCommandHandler();
            return commandHandler.Handle(command); 
        }
        
    }
}