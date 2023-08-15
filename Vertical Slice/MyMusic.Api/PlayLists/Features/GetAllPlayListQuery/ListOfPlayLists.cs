using System.Collections.Generic;
using MyMusic.PlayLists.Features.GetPlayListQuery;

namespace MyMusic.PlayLists.Features.GetAllPlayListQuery {

    public class ListOfPlayLists {
        public List<PlayList> Elements { get; }

        public ListOfPlayLists(List<PlayList> elements) {
            Elements = elements;
        }
    }
}