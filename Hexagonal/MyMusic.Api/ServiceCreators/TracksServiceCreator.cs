using MyMusic.Application.Services;
using MyMusic.Infrastructure.Http;
using MyMusic.Infrastructure.Persistence;

namespace MyMusic.ServiceCreators {

    public class TracksServiceCreator {
        public GetTracksService CreateGetTrackService() {
            var tracksDatabaseAdapter = new TracksDatabaseAdapter();
            return new GetTracksService(tracksDatabaseAdapter);
        }
        
        public AddTrackToPlayListService CreateAddTrackToPlayListService() {
            var tracksDatabaseAdapter = new TracksDatabaseAdapter();
            var musicCloudApiHttpAdapter = new MusicCloudApiHttpAdapter();
            return new AddTrackToPlayListService(tracksDatabaseAdapter, musicCloudApiHttpAdapter);
        }
        
        public DeleteTrackFromPLayListService CreateDeleteTrackFromPLayListService() {
            var tracksDatabaseAdapter = new TracksDatabaseAdapter();
            var musicCloudApiHttpAdapter = new MusicCloudApiHttpAdapter();
            return new DeleteTrackFromPLayListService(tracksDatabaseAdapter, musicCloudApiHttpAdapter);
        }
    }
}