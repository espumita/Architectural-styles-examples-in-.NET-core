using System.Collections.Generic;
using MyMusic.Domain;

namespace MyMusic.Application.Services {

    public class PlayListList {
        public List<PlayList> playLists { get; }

        public PlayListList(List<PlayList> playLists) {
            this.playLists = playLists;
        }
    }
}