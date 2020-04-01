using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using MyMusic.Domain.Errors;

namespace MyMusic.Application.Services {
    public class GetTracksService {
        
        private readonly TracksPersistencePort tracksPersistence;

        public GetTracksService(TracksPersistencePort tracksPersistence) {
            this.tracksPersistence = tracksPersistence;
        }

        public Either<DomainError, Track> Get(string trackId) {
            return tracksPersistence.GetTrack(trackId);
        }
    }
}