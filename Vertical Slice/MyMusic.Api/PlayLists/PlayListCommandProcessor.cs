using LanguageExt;
using MyMusic.PlayLists.Domain.Error;
using MyMusic.Shared.Commands.Successes;

namespace MyMusic.PlayLists {

    public class PlayListCommandProcessor {
        private readonly PlayListCommandHandlerCreator playListCommandHandlerCreator;

        public PlayListCommandProcessor(PlayListCommandHandlerCreator playListCommandHandlerCreator) {
            this.playListCommandHandlerCreator = playListCommandHandlerCreator;
        }

        public Either<DomainError, CommandResult> Process(Features.CreatePLayList.CreatePLayList command) {
            var commandHandler = playListCommandHandlerCreator.CreateCreatePlayListCommandHandler();
            return commandHandler.Handle(command);
        }
        
        public Either<DomainError, CommandResult> Process(Features.RenamePlaylist.RenamePlaylist command) {
            var commandHandler = playListCommandHandlerCreator.CreateRenamePlayListCommandHandler();
            return commandHandler.Handle(command);
        }
        
        public Either<DomainError, CommandResult> Process(Features.ChangePlayListImageUrl.ChangePlayListImageUrl command) {
            var commandHandler = playListCommandHandlerCreator.CreateAddImageUrlPlayListCommandHandler();
            return commandHandler.Handle(command);
        }
        public Either<DomainError, CommandResult> Process(Features.ArchivePlayList.ArchivePlayList command) {
            var commandHandler = playListCommandHandlerCreator.CreateArchivePlayListCommandHandler();
            return commandHandler.Handle(command); 
        }
        
    }
}