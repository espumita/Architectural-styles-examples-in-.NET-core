using MyMusic.Domain;

namespace MyMusic.Application.Write.Ports.Persistence {
    public interface PlayListPersistencePort {
        
        PlayList GetPlayList(string playlistId);
        
        void Persist(PlayList playList);
    }
}