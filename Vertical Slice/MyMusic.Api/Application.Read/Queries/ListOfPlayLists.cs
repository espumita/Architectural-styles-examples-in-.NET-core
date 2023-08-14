using System.Collections.Generic;
using MyMusic.Application.Read.Model;

namespace MyMusic.Application.Read.Queries {

    public class ListOfPlayLists {
        public List<PlayList> Elements { get; }

        public ListOfPlayLists(List<PlayList> elements) {
            Elements = elements;
        }
    }
}