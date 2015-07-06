using FileRandomizer3000.Core.Services.Interfaces;
using System;
using System.Threading;

namespace FileRandomizer3000.Core.Services
{
    public class SynchronizationContextAdapter : IContext
    {
        private SynchronizationContext _context;

        public SynchronizationContextAdapter(SynchronizationContext context)
        {
            _context = context;
        }

        public virtual void Post(SendOrPostCallback d, Object state)
        {
            _context.Post(d, state);
        }
    }
}