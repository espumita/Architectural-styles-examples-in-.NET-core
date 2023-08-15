namespace MyMusic.PlayLists.Features {
    public interface PlayListPersistence {
        
        Domain.PlayList GetPlayList(string playlistId);
        
        void Persist(Domain.PlayList playList);
    }
}