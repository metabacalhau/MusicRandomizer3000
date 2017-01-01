using MusicRandomizer3000.Core.Enums;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface IBackgroundWorkerFactory
    {
        IBackgroundWorker GetWorker(BackgroundWorkerType workerType);
    }
}