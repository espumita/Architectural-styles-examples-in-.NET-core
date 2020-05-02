namespace MyMusic.Application.Commands {
    public class CreatePLayList : Command {
        public string playListName { get; }

        public CreatePLayList(string playListName) {
            this.playListName = playListName;
        }
    }
}