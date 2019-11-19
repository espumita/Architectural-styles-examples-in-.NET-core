namespace MyMusic.Model {
    public class PlayList {
        public string Id { get; }
        public string Name { get; }

        public PlayList(string id, string name) {
            Id = id;
            Name = name;
        }
    }
}