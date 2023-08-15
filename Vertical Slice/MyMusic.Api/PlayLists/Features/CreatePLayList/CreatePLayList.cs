using MyMusic.Shared.Commands;

namespace MyMusic.PlayLists.Features.CreatePLayList {
    public class CreatePLayList : Command {
        public string PlayListName { get; }

        public CreatePLayList(string playListName) {
            PlayListName = playListName;
        }
    }
}