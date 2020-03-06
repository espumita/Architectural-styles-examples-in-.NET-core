using System.Collections.Generic;
using MyMusic.Domain;

namespace MyMusic.Application.Services {

    public class ListOfPlayLists {
        public List<PlayList> elements { get; }

        public ListOfPlayLists(List<PlayList> elements) {
            this.elements = elements;
        }
    }
}