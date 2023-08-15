using System.Collections.Generic;
using MyMusic.PlayLists.Features.GetPlayListQuery;

namespace MyMusic.PlayLists.Features {

    public interface PlayListQueryPort {
        PlayList GetPlayList(string playlistId);
        
        List<PlayList> GetAllPlayList();
    }
}