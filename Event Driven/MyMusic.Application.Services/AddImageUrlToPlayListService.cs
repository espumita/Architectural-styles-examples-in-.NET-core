using LanguageExt;
using MyMusic.Application.Ports;
using MyMusic.Application.Ports.Persistence;
using MyMusic.Application.Services.Successes;
using MyMusic.Domain.Error;

namespace MyMusic.Application.Services {
    public class AddImageUrlToPlayListService {
        
        private readonly PlayListPersistencePort playListPersistence;
        private readonly EventPublisherPort eventPublisher;

        public AddImageUrlToPlayListService(PlayListPersistencePort playListPersistence, EventPublisherPort eventPublisher) {
            this.playListPersistence = playListPersistence;
            this.eventPublisher = eventPublisher;
        }

        public Either<DomainError, ServiceResponse> Execute(string playListId, string aNewImageUrL) {
            var playList = playListPersistence.GetPlayList(playListId);
            playList.AddImageUrl(aNewImageUrL);

            playListPersistence.Persist(playList);
            eventPublisher.Publish(playList.Events());
            return ServiceResponse.Success;
        }
    }
}