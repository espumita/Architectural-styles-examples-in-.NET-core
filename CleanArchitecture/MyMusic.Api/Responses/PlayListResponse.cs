using MyMusic.Model;

namespace MyMusic.Responses {
    public class PlayListResponse {
        public string PlayListId { get; }
        public string PlayListName { get; }

        private PlayListResponse(string playListId, string playListName) {
            PlayListId = playListId;
            PlayListName = playListName;
        }

        public static PlayListResponse From(PlayList playList) {
            return new PlayListResponse(playList.Id, playList.Name);
        }
    }
}