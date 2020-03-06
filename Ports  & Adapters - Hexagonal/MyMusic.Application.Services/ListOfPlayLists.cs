using System.Collections.Generic;
using MyMusic.Domain;

namespace MyMusic.Application.Services {

    public class ListOfPlayLists {
        public List<PlayList> Elements { get; }

        public ListOfPlayLists(List<PlayList> elements) {
            Elements = elements;
        }
    }
}