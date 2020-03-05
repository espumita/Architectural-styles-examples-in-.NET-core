using MyMusic.Domain;

namespace MyMusic.Application.Ports.Persistence {
    
    public interface PlayListPersistencePort {
        PlayList GetPlayList(string playlistId);
        string CreatePlayListFrom(string playListName);
        void DeletePlayList(object playListId);
        void Persist(PlayList playList);
    }
}