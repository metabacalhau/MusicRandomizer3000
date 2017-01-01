using System;
using System.Threading;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface IContext
    {
        void Post(SendOrPostCallback d, Object state);
    }
}