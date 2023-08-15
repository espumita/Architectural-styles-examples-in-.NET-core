using MyMusic.Shared.Domain;

namespace MyMusic.Shared.Persistence {
    public interface PlayListPersistence {
        
        PlayList GetPlayList(string playlistId);
        
        void Persist(PlayList playList);
    }
}