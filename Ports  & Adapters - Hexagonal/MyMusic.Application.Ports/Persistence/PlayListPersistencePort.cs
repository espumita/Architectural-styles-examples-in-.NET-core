using MyMusic.Domain;

namespace MyMusic.Application.Ports.Persistence {
    
    public interface PlayListPersistencePort {
        PlayList GetPlayList(string playlistId);
        void Persist(PlayList playList);
    }
}