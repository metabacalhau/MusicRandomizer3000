using FileRandomizer3000.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FileRandomizer3000.Core.Services.Interfaces
{
    public interface IRandomizerWorker
    {
        void Run(RandomizerWorkerSettings settings, Action<List<AppFile>> onFinished, Action<string> onFailed, Action onCancelled);
        void Cancel();
    }
}