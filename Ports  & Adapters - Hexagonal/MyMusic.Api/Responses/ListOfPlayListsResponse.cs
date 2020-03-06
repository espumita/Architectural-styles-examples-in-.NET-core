using System.Collections.Generic;
using System.Linq;
using MyMusic.Application.Services;

namespace MyMusic.Responses {

    public class ListOfPlayListsResponse : ResponseBuilder<ListOfPlayListsResponse, ListOfPlayLists>{
        
 
        public List<PlayListResponse> PlayLists { get; private set; }
    
        public ListOfPlayListsResponse() {}

        public ListOfPlayListsResponse BuildFrom(ListOfPlayLists listOfPlayLists) {
            PlayLists = listOfPlayLists.elements.Select(playList => new PlayListResponse().BuildFrom(playList)).ToList();
            return this;
        }
    }
    
}