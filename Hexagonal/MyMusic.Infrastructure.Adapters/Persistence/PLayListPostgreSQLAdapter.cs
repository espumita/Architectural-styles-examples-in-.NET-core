using System.Collections.Generic;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;

namespace MyMusic.Infrastructure.Adapters.Persistence {
    public class PLayListPostgreSQLAdapter : PlayListPersistencePort {
        public PlayList GetPlayList(string playlistId) {
            //This should be read from PostgreSQL DB
            var trackList = new List<Track> {
                new Track("D7D0BF31-CC98-44EA-B983-C8C37FA95A59", "Hakujitsu", "King Gnu",261000),
                new Track("560D59E0-0487-4DF5-90C6-95C5594F244A", "Era - Ameno (The Scientist Remix)", "The Scientist DJ", 202200)
            };
            return new PlayList(playlistId, "Example PlayList", trackList, "https://imageUrl.com");
        }

        public string CreatePlayListFrom(string playListName) {
            //This should save in PostgreSQL DB
            var newPlayListId = "B7875AD5-FDC3-4067-9902-0072226552DD";
            return newPlayListId;
        }

        public void ChangePlayListName(string playListId, string newPlayListName) {
            //This should update in PostgreSQL DB
        }

        public void DeletePlayList(object playListId) {
            //This should delete in PostgreSQL DB
        }

        public void Persist(PlayList playList) {
            throw new System.NotImplementedException();
        }
    }
}