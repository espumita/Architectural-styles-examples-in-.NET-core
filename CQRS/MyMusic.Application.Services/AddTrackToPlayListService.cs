using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using System.Linq;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Application.SharedKernel.Model;

namespace MyMusic.Application.Services {
    public class AddTrackToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistencePort;
        private readonly TracksNotifierPort tracksNotifier;
        
        public AddTrackToPlayListService(PlayListPersistencePort playListPersistencePort, TracksNotifierPort tracksNotifier) {
            this.playListPersistencePort = playListPersistencePort;
            this.tracksNotifier = tracksNotifier;
        }

        public Either<Error, ServiceResponse> Execute(string trackId, string playlistId) {
            var playList = playListPersistencePort.GetPlayList(playlistId);
            if (TrackIsAlreadyIn(playList, trackId)) return ServiceError.CannotAddSameTrackTwice; 
            playList.Add(new Track(trackId, "", "", 1));
            playListPersistencePort.Persist(playList);
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(trackId, playlistId);
            return ServiceResponse.Success;
        }

        private bool TrackIsAlreadyIn(PlayList playList, string trackId) {
            return playList.TrackList.FirstOrDefault(x => x.Id.Equals(trackId)) != null;
        }
    }

}