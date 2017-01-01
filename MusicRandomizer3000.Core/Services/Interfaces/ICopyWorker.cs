using MusicRandomizer3000.Core.Models;
using System;
using System.Collections.Generic;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface ICopyWorker
    {
        void Run(CopyWorkerParameters parameters);
        void Cancel();
    }
}