using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Services.Interfaces;

namespace FileRandomizer3000.Core.Services
{
    public class BackgroundWorkerFactory : IBackgroundWorkerFactory
    {
        public IBackgroundWorker GetWorker(BackgroundWorkerType workerType)
        {
            IBackgroundWorker result = null;

            if (workerType == BackgroundWorkerType.Sync)
            {
                result = new BackgroundWorkerSync();
            }
            else if (workerType == BackgroundWorkerType.Async)
            {
                result = new BackgroundWorkerAsync();
            }

            return result;
        }
    }
}