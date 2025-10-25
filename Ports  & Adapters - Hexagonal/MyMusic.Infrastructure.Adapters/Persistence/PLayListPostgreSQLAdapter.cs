using System.Collections.Generic;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;

namespace MyMusic.Infrastructure.Adapters.Persistence {
    public class PlayListPostgreSQLAdapter : PlayListPersistencePort {
        
        public PlayList GetPlayList(string playlistId) {
            //This should be read from PostgreSQL DB
            var trackList = new List<Track> {
                new Track("D7D0BF31-CC98-44EA-B983-C8C37FA95A59", "Hakujitsu", "King Gnu",261000),
                new Track("560D59E0-0487-4DF5-90C6-95C5594F244A", "Era - Ameno (The Scientist Remix)", "The Scientist DJ", 202200)
            };
            return new PlayList(playlistId, "Example PlayList", PlayListStatus.Active, trackList, "https://imageUrl.com");
        }

        public List<PlayList> GetAllPlayList() {
            var trackList = new List<Track> {
                new Track("D7D0BF31-CC98-44EA-B983-C8C37FA95A59", "Hakujitsu", "King Gnu",261000),
                new Track("560D59E0-0487-4DF5-90C6-95C5594F244A", "Era - Ameno (The Scientist Remix)", "The Scientist DJ", 202200)
            };
            return new List<PlayList> {
                new PlayList("B6AED672-2663-49BA-85B0-0DDB02D59C1B", "Example PlayList", PlayListStatus.Active, new List<Track>(), "https://imageUrl.com"),
                new PlayList("916A3E10-7AF2-4D54-BD78-48364F783F78", "Example PlayList 2", PlayListStatus.Active, trackList, "https://imageUrl2.com"),
                new PlayList("BF2D7788-D1FE-4772-B362-6D89686D895A", "Example PlayList 3", PlayListStatus.Archived, new List<Track>(), "https://imageUrl3.com"),
            };
        }

        public void Persist(PlayList playList) {
            //This should persist in PostgreSQL DB
        }
    }
}