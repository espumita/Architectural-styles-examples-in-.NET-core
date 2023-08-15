using MyMusic.Shared.Commands;

namespace MyMusic.PlayList.Features.CreatePLayList {
    public class CreatePLayList : Command {
        public string PlayListName { get; }

        public CreatePLayList(string playListName) {
            PlayListName = playListName;
        }
    }
}