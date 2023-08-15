using System.Collections.Generic;
using MyMusic.PlayLists.Features.GetPlayListQuery;

namespace MyMusic.PlayLists.Features {

    public interface PlayListQuery {
        PlayList GetPlayList(string playlistId);
        
        List<PlayList> GetAllPlayList();
    }
}