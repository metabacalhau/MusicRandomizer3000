using FileRandomizer3000.Core;
using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Services;
using FileRandomizer3000.Core.Services.Interfaces;
using NUnit.Framework;
using System;

namespace FileRandomizer3000.Tests.UnitTests.Services
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