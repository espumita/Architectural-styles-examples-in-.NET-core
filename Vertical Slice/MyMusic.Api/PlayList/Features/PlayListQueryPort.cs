using System.Collections.Generic;

namespace MyMusic.PlayList.Features {

    public interface PlayListQueryPort {
        PlayList.Features.GetPlayListQuery.PlayList GetPlayList(string playlistId);
        
        List<PlayList.Features.GetPlayListQuery.PlayList> GetAllPlayList();
    }
}