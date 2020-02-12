namespace MyMusic.Model.PortsContracts.Notifications {

    public interface PlayListNotifierPort {
        void NotifyPlayListHasBeenCreated(string playListId, string playListName);
        void NotifyPlayListNameHasChanged(string playListId, string newPlayListName);
        void NotifyPlayListHasBeenDeleted(string playListId);

    }
}