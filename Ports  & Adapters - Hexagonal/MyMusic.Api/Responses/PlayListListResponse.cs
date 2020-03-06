using System.Collections.Generic;
using System.Linq;
using MyMusic.Application.Services;

namespace MyMusic.Responses {

    public class PlayListListResponse : ResponseBuilder<PlayListListResponse, PlayListList>{
        
 
        public List<PlayListResponse> PlayLists { get; private set; }
    
        public PlayListListResponse() {}

        public PlayListListResponse BuildFrom(PlayListList playListList) {
            PlayLists = playListList.playLists.Select(playList => new PlayListResponse().BuildFrom(playList)).ToList();
            return this;
        }
    }
    
}