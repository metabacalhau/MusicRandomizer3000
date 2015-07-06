using FileRandomizer3000.Core.Services.Interfaces;
using System.Threading;

namespace FileRandomizer3000.Tests.Helpers
{
    // need this when I use BackgroundWorker
    // its purpose is to execute the expression directly,
    // if I setup the Post function to match the expression it will always fail,
    // since Moq will not recognize the expression that needs to be executed
    public class ContextStub : IContext
    {
        public void Post(SendOrPostCallback d, object state)
        {
            d(state);
        }
    }
}
