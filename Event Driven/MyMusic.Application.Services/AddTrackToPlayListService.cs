using LanguageExt;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using System.Linq;
using MyMusic.Application.Ports;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Events;

namespace MyMusic.Application.Services {
    public class AddTrackToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventBusPort eventBus;
        
        public AddTrackToPlayListService(PlayListPersistencePort playListPersistence, EventBusPort eventBus) {
            this.playListPersistence = playListPersistence;
            this.eventBus = eventBus;
        }

        public Either<ServiceError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistence.GetPlayList(playlistId);
            if (TrackIsAlreadyIn(playList, trackId)) return ServiceError.CannotAddSameTrackTwice; 
            playList.Add(Track.With(trackId));
            playListPersistence.Persist(playList);
            eventBus.Raise(new TrackHasBeenAddedToPlayList(trackId, playlistId));
            return ServiceResponse.Success;
        }

        private bool TrackIsAlreadyIn(PlayList playList, string trackId) {
            return playList.TrackList.FirstOrDefault(x => x.Id.Equals(trackId)) != null;
        }
    }

}