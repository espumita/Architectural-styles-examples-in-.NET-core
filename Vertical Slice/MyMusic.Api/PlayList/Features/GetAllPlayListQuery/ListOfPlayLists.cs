using System.Collections.Generic;

namespace MyMusic.PlayList.Features.GetAllPlayListQuery {

    public class ListOfPlayLists {
        public List<PlayList.Features.GetPlayListQuery.PlayList> Elements { get; }

        public ListOfPlayLists(List<PlayList.Features.GetPlayListQuery.PlayList> elements) {
            Elements = elements;
        }
    }
}