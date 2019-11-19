namespace MyMusic.Model {
    public class PlayList {
        public string Id { get; private set; }

        public PlayList(string id) {
            Id = id;
        }
    }
}