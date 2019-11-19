namespace MyMusic.Model.PortsContracts.Persistence {
    
    public interface PlayListPersistencePort {
        PlayList GetPlayList(string playlistId);
        void CreatePlayListFrom(string playListName);
        void ChangePlayListName(string playListId, string newPlayListName);
        void DeletePlayList(object playListId);
    }
}