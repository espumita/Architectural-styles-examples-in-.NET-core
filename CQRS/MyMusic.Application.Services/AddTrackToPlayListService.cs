using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using System.Linq;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;

namespace MyMusic.Application.Services {
    public class AddTrackToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly TracksNotifierPort tracksNotifier;
        
        public AddTrackToPlayListService(PlayListPersistencePort playListPersistence, TracksNotifierPort tracksNotifier) {
            this.playListPersistence = playListPersistence;
            this.tracksNotifier = tracksNotifier;
        }

        public Either<ServiceError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistence.GetPlayList(playlistId);
            if (TrackIsAlreadyIn(playList, trackId)) return ServiceError.CannotAddSameTrackTwice; 
            playList.Add(Track.With(trackId));
            playListPersistence.Persist(playList);
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(trackId, playlistId);
            return ServiceResponse.Success;
        }

        private bool TrackIsAlreadyIn(PlayList playList, string trackId) {
            return playList.TrackList.FirstOrDefault(x => x.Id.Equals(trackId)) != null;
        }
    }

}