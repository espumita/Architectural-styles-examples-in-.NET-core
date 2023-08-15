using MyMusic.PlayLists.Features.GetPlayListQuery;

namespace MyMusic.Tracks.Features {

    public class TracksPostgreSQLQueries : TracksQuery {
        public Track GetTrack(string trackId) {
            //This should be read from PostgreSQL DB
            return new Track("2E5804A7-A0CC-46E0-B167-A818A696F3E0", "Mis Colegas", "Ska-P", 246600);
        }
    }
}