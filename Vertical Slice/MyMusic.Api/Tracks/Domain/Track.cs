namespace MyMusic.Tracks.Domain {
    public class Track {
        public string Id { get; }

        private Track(string id) {
            Id = id;
        }

        public static Track With(string trackId) {
            return new Track(trackId);
        }
    }
}