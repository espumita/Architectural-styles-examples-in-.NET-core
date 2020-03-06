using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Domain;

namespace MyMusic.Application.Services {
    public class GetTracksService {
        
        private readonly TracksPersistencePort tracksPersistence;

        public GetTracksService(TracksPersistencePort tracksPersistence) {
            this.tracksPersistence = tracksPersistence;
        }

        public Either<PlayListError, Track> Get(string trackId) {
            return tracksPersistence.GetTrack(trackId);
        }
    }
}