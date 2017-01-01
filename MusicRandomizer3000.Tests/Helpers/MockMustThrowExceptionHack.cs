using Moq;
using System;
using System.Reflection;

namespace MusicRandomizer3000.Tests.Helpers
{
    public class MockMustThrowExceptionHack
    {
        public static void ExecuteMock(Action action)
        {
            try
            {
                action();
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }

                throw;
            }
        }
    }
}
