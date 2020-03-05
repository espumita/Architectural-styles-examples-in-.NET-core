using LanguageExt;
using MyMusic.Application.Ports.Notifications;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Domain;
using System.Linq;

namespace MyMusic.Application.Services {
    
    public class AddTrackToPlayListService {
        private readonly PlayListPersistencePort playListPersistencePort;
        private readonly TracksNotifierPort tracksNotifier;

        public AddTrackToPlayListService(PlayListPersistencePort playListPersistencePort, TracksNotifierPort tracksNotifier) {
            this.playListPersistencePort = playListPersistencePort;
            this.tracksNotifier = tracksNotifier;
        }

        public Either<PlayListError, int> Execute(string trackId, string playlistId) {
            var playList = playListPersistencePort.GetPlayList(playlistId);
            var focusTrack = playList.TrackList.FirstOrDefault(x => x.Id.Equals(trackId));
            if (focusTrack != null) return PlayListError.CannotAddSameTrackTwice; 
            playList.Add(new Track(trackId, "", "", 1));
            playListPersistencePort.Persist(playList);
            tracksNotifier.NotifyTrackHasBeenAddedToPlayList(trackId, playlistId);
            return 0;
        }
        
    }

}