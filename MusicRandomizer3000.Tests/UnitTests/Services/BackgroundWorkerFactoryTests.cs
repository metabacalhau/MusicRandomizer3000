using MusicRandomizer3000.Core;
using MusicRandomizer3000.Core.Enums;
using MusicRandomizer3000.Core.Services;
using MusicRandomizer3000.Core.Services.Interfaces;
using NUnit.Framework;
using System;

namespace MusicRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class BackgroundWorkerFactoryTests
    {
        IBackgroundWorkerFactory InitializeFactory()
        {
            return new BackgroundWorkerFactory();
        }

        [TestCase(BackgroundWorkerType.Async, typeof(BackgroundWorkerAsync))]
        [TestCase(BackgroundWorkerType.Sync, typeof(BackgroundWorkerSync))]
        public void GetWorker_SupplyDesiredWorkerType_ReturnsRespectiveWorkerInstance(BackgroundWorkerType enumType, Type expectedType)
        {
            // arrange
            IBackgroundWorkerFactory factory = InitializeFactory();

            // act
            IBackgroundWorker result = factory.GetWorker(enumType);

            // assert
            Assert.IsInstanceOf(expectedType, result);
        }
    }
}