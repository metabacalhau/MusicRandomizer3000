using FileRandomizer3000.Core.Enums;

namespace FileRandomizer3000.Core.Services.Interfaces
{
    public interface IBackgroundWorkerFactory
    {
        IBackgroundWorker GetWorker(BackgroundWorkerType workerType);
    }
}