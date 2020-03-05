namespace MyMusic.Domain {
    public class Track {
        public string Id { get; }
        public string Name { get; }
        public string Artist { get; }
        public int DurationInMs { get; }

        public Track(string id, string name, string artist, int durationInMs) {
            Id = id;
            Name = name;
            Artist = artist;
            DurationInMs = durationInMs;
        }
    }
}