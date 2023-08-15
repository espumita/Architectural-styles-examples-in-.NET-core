namespace MyMusic.PlayList.Features {
    public interface PlayListPersistencePort {
        
        Domain.PlayList GetPlayList(string playlistId);
        
        void Persist(Domain.PlayList playList);
    }
}