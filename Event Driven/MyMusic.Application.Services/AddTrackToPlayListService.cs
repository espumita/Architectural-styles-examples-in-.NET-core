using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using LanguageExt.UnsafeValueAccess;
using MyMusic.Application.Ports;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Error;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Services {
    public class AddTrackToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventBusPort eventBus;
        
        public AddTrackToPlayListService(PlayListPersistencePort playListPersistence, EventBusPort eventBus) {
            this.playListPersistence = playListPersistence;
            this.eventBus = eventBus;
        }
        
        public Either<DomainError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistence.GetPlayList(playlistId);
            var error = playList.Add(Track.With(trackId));
            if (error.IsSome) return error.ValueUnsafe();
            
            playListPersistence.Persist(playList);
            eventBus.Raise(new TrackHasBeenAddedToPlayList(trackId, playList.Id));
            return ServiceResponse.Success;
        }
        
    }

}