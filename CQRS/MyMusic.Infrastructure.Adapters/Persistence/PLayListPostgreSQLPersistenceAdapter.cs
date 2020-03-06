using System.Collections.Generic;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;

namespace MyMusic.Infrastructure.Adapters.Persistence {
    public class PLayListPostgreSQLPersistenceAdapter : PlayListPersistencePort {
        
        public PlayList GetPlayList(string playlistId) {
            //This should be read from PostgreSQL DB
            var trackList = new List<Track> {
                new Track("D7D0BF31-CC98-44EA-B983-C8C37FA95A59", "Hakujitsu", "King Gnu",261000),
                new Track("560D59E0-0487-4DF5-90C6-95C5594F244A", "Era - Ameno (The Scientist Remix)", "The Scientist DJ", 202200)
            };
            return new PlayList(playlistId, "Example PlayList", PlayListStatus.Active, trackList, "https://imageUrl.com");
        }

        public void Persist(PlayList playList) {
            //This should persist in PostgreSQL DB
        }
    }
}