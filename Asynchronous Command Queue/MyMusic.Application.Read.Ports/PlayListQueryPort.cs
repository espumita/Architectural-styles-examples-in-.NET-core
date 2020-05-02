using System.Collections.Generic;
using MyMusic.Application.Read.Model;

namespace MyMusic.Application.Read.Ports {

    public interface PlayListQueryPort {
        PlayList GetPlayList(string playlistId);
        
        List<PlayList> GetAllPlayList();
    }
}