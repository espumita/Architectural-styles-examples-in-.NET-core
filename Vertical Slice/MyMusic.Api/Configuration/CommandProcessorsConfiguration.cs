using Microsoft.Extensions.DependencyInjection;
using MyMusic.Application.Write.Commands;
using MyMusic.Application.Write.Ports;
using MyMusic.CommandProcessors;

namespace MyMusic.Configuration {

    public static class CommandProcessorsConfiguration {
        public static void Configure(IServiceCollection services, CommandQueuePort commandQueue) {
            RegisterPlayListCommandProcessorsInToDependencyInjector(services, commandQueue);
            RegisterTrackCommandProcessorsInToDependencyInjector(services, commandQueue);
        }
        
        private static void RegisterPlayListCommandProcessorsInToDependencyInjector(IServiceCollection services, CommandQueuePort commandQueue) {
            services.AddSingleton<PlayListCommandProcessor>();
            var playListCommandProcessor = services.BuildServiceProvider().GetService<PlayListCommandProcessor>();
            RegisterPlayListCommandProcessorsInTo(commandQueue, playListCommandProcessor);
        }

        private static void RegisterPlayListCommandProcessorsInTo(CommandQueuePort commandQueue, PlayListCommandProcessor playListCommandProcessor) {
            commandQueue.SetQueueSingleConsumer<CreatePLayList>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<RenamePlaylist>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<ChangePlayListImageUrl>(playListCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<ArchivePlayList>(playListCommandProcessor.Process);
        }
        
        private static void RegisterTrackCommandProcessorsInToDependencyInjector(IServiceCollection services, CommandQueuePort commandQueue) {
            services.AddSingleton<TrackCommandProcessor>();
            var trackCommandProcessor = services.BuildServiceProvider().GetService<TrackCommandProcessor>();
            RegisterTrackCommandProcessorsInTo(commandQueue, trackCommandProcessor);
        }

        private static void RegisterTrackCommandProcessorsInTo(CommandQueuePort commandQueue, TrackCommandProcessor trackCommandProcessor) {
            commandQueue.SetQueueSingleConsumer<AddTrackToPLayList>(trackCommandProcessor.Process);
            commandQueue.SetQueueSingleConsumer<RemoveTrackFromPlayList>(trackCommandProcessor.Process);

        }
    }
}