using MyMusic.Application.Commands;
using MyMusic.CommandHandlerCreators;

namespace MyMusic.CommandProcessors {

    public class PlayListCommandProcessor {
        private readonly PlayListCommandHandlerCreator playListCommandHandlerCreator;

        public PlayListCommandProcessor(PlayListCommandHandlerCreator playListCommandHandlerCreator) {
            this.playListCommandHandlerCreator = playListCommandHandlerCreator;
        }

        public void Process(CreatePLayList command) {
            var commandHandler = playListCommandHandlerCreator.CreateCreatePlayListCommandHandler();
            var result = commandHandler.Handle(command);
        }
        
        public void Process(RenamePlaylist command) {
            var commandHandler = playListCommandHandlerCreator.CreateRenamePlayListCommandHandler();
            var result = commandHandler.Handle(command);
        }
        
    }
}