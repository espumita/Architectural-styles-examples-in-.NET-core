namespace MyMusic.Model.PortsContracts {
    public interface TracksPersistencePort {
        void AddTrackToPlayList(string trackId, string playlistId);
        void DeleteTrackFromPlayList(string trackId, string playlistId);
        Track GetTrack(string trackId);
    }
}