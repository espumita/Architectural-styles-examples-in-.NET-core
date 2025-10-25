namespace MyMusic.Application.Commands {
    public class CreatePlayList : Command {
        public string PlayListName { get; }

        public CreatePlayList(string playListName) {
            PlayListName = playListName;
        }
    }
}