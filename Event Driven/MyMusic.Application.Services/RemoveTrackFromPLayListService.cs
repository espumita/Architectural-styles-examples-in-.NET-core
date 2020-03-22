using System.Linq;
using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Services {
    public class RemoveTrackFromPLayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventBusPort eventBus;
        
        public RemoveTrackFromPLayListService(PlayListPersistencePort playListPersistence, EventBusPort eventBus) {
            this.playListPersistence = playListPersistence;
            this.eventBus = eventBus;
        }

        public Either<ServiceError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistence.GetPlayList(playlistId);
            if (TrackIsNotAlreadyIn(playList, trackId)) return ServiceError.TrackIsNotInThePlayList;
            playList.Remove(trackId);
            playListPersistence.Persist(playList);
            eventBus.Raise(new TrackHasBeenRemovedFromPlayList(trackId, playList.Id));
            return ServiceResponse.Success;
        }
        
        private bool TrackIsNotAlreadyIn(PlayList playList, string trackId) {
            return playList.TrackList.FirstOrDefault(x => x.Id.Equals(trackId)) == null;
        }
        
    }
}