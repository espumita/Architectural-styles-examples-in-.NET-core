using System.Collections.Generic;
using MyMusic.Model;
using MyMusic.Model.PortsContracts.Persistence;

namespace MyMusic.Infrastructure.Persistence {
    public class PLayListDatabaseAdapter : PlayListPersistencePort {
        public PlayList GetPlayList(string playlistId) {
            //This should be read from persistence
            var trackList = new List<Track> {
                new Track("D7D0BF31-CC98-44EA-B983-C8C37FA95A59", "Hakujitsu", "King Gnu",261000),
                new Track("560D59E0-0487-4DF5-90C6-95C5594F244A", "Era - Ameno (The Scientist Remix)", "The Scientist DJ", 202200)
            };
            return new PlayList(playlistId, "Example PlayList", trackList, "https://imageUrl.com");
        }

        public void CreatePlayListFrom(string playListName) {
            //This should save in persistence
        }

        public void ChangePlayListName(string playListId, string newPlayListName) {
            //This should update in persistence
        }

        public void DeletePlayList(object playListId) {
            //This should delete in persistence
        }
    }
}