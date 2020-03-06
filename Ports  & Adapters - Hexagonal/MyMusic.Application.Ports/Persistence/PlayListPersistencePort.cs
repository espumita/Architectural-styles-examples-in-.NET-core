using System.Collections.Generic;
using MyMusic.Domain;

namespace MyMusic.Application.Ports.Persistence {
    public interface PlayListPersistencePort {
        
        PlayList GetPlayList(string playlistId);
        
        List<PlayList> GetAllPlayList();
        void Persist(PlayList playList);
    }
}