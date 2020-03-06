using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using System.Linq;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;

namespace MyMusic.Application.Services {
    public class AddTrackToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistencePort;
        private readonly TracksNotifierPort tracksNotifier;
        
        public AddTrackToPlayListService(PlayListPersistencePort playListPersistencePort, TracksNotifierPort tracksNotifier) {
            this.playListPersistencePort = playListPersistencePort;
            this.tracksNotifier = tracksNotifier;
        }

        public Either<PlayListError, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistencePort.GetPlayList(playlistId);
            if (TrackIsAlreadyIn(playList, trackId)) return PlayListError.CannotAddSameTrackTwice; 
            playList.Add(new Track(trackId, "", "", 1));
            playListPersistencePort.Persist(playList);
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(trackId, playlistId);
            return ServiceResponse.OperationSuccess;
        }

        private bool TrackIsAlreadyIn(PlayList playList, string trackId) {
            return playList.TrackList.FirstOrDefault(x => x.Id.Equals(trackId)) != null;
        }
    }

}