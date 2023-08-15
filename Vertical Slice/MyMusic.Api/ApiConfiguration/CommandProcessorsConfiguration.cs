using Microsoft.Extensions.DependencyInjection;
using MyMusic.PlayLists;
using MyMusic.PlayLists.Features.ArchivePlayList;
using MyMusic.PlayLists.Features.ChangePlayListImageUrl;
using MyMusic.PlayLists.Features.CreatePLayList;
using MyMusic.PlayLists.Features.RenamePlaylist;
using MyMusic.Shared.Infrastructure;
using MyMusic.Tracks;
using MyMusic.Tracks.Features.AddTrackToPLayList;
using MyMusic.Tracks.Features.RemoveTrackFromPlayList;

namespace MyMusic.ApiConfiguration {

    public static class CommandProcessorsConfiguration {
        public static void Configure(IServiceCollection services, CommandQueue commandQueue) {
            RegisterPlayListCommandProcessorsInToDependencyInjector(services, commandQueue);
            RegisterTrackCommandProcessorsInToDependencyInjector(services, commandQueue);
        }
        
        private static void RegisterPlayListCommandProcessorsInToDependencyInjector(IServiceCollection services, CommandQueue commandQueue) {
            services.AddSingleton<PlayListCommandProcessor>();
            var playListCommandProcessor = services.BuildServiceProvider().GetService<PlayListCommandProcessor>();
            RegisterPlayListCommandProcessorsInTo(commandQueue, playListCommandProcessor);
        }

        private static void RegisterPlayListCommandProcessorsInTo(CommandQueue commandQueue, PlayListCommandProcessor playListCommandProcessor) {
            commandQueue.SetQueueSingleConsumer<CreatePLayList>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<RenamePlaylist>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<ChangePlayListImageUrl>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<ArchivePlayList>(playListCommandProcessor.Process);
        }
        
        private static void RegisterTrackCommandProcessorsInToDependencyInjector(IServiceCollection services, CommandQueue commandQueue) {
            services.AddSingleton<TrackCommandProcessor>();
            var trackCommandProcessor = services.BuildServiceProvider().GetService<TrackCommandProcessor>();
            RegisterTrackCommandProcessorsInTo(commandQueue, trackCommandProcessor);
        }

        private static void RegisterTrackCommandProcessorsInTo(CommandQueue commandQueue, TrackCommandProcessor trackCommandProcessor) {
            commandQueue.SetQueueSingleConsumer<AddTrackToPLayList>(trackCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<RemoveTrackFromPlayList>(trackCommandProcessor.Process);

        }
    }
}