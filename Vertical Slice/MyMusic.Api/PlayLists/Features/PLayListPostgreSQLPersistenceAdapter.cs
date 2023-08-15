using System.Collections.Generic;
using MyMusic.PlayLists.Domain;
using MyMusic.Tracks.Domain;

namespace MyMusic.PlayLists.Features {
    public class PLayListPostgreSQLPersistenceAdapter : PlayListPersistencePort {
        
        public Domain.PlayList GetPlayList(string playlistId) {
            //This should be read from PostgreSQL DB
            var trackList = new List<Track> {
                Track.With("D7D0BF31-CC98-44EA-B983-C8C37FA95A59"),
                Track.With("560D59E0-0487-4DF5-90C6-95C5594F244A")
            };
            return new Domain.PlayList(playlistId, "Example PlayList", PlayListStatus.Active, trackList, "https://imageUrl.com");
        }

        public void Persist(Domain.PlayList playList) {
            //This should persist in PostgreSQL DB
        }
    }
}