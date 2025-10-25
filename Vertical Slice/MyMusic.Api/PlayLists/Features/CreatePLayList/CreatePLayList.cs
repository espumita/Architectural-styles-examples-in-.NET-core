using MyMusic.Shared;

namespace MyMusic.PlayLists.Features.CreatePlayList {
    public class CreatePlayList : Command {
        public string PlayListName { get; }

        public CreatePlayList(string playListName) {
            PlayListName = playListName;
        }
    }
}