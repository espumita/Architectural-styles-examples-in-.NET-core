namespace MyMusic.Application.Commands {
    public class CreatePLayList : Command {
        public string PlayListName { get; }

        public CreatePLayList(string playListName) {
            PlayListName = playListName;
        }
    }
}