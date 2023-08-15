using System.Collections.Generic;
using System.Linq;
using MyMusic.PlayList.Features.GetPlayListQuery;
using MyMusic.Shared;

namespace MyMusic.PlayList.Features.GetAllPlayListQuery {

    public class ListOfPlayListsResponse : ResponseBuilder<ListOfPlayListsResponse, ListOfPlayLists>{
        
 
        public List<PlayListResponse> PlayLists { get; private set; }
    
        public ListOfPlayListsResponse() {}

        public ListOfPlayListsResponse BuildFrom(ListOfPlayLists listOfPlayLists) {
            PlayLists = listOfPlayLists.Elements.Select(playList => new PlayListResponse().BuildFrom(playList)).ToList();
            return this;
        }
    }
    
}