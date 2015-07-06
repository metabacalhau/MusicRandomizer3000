using FileRandomizer3000.Core;
using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Services;
using FileRandomizer3000.Core.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel;

namespace FileRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class BackgroundWorkerTests
    {
        IBackgroundWorker InitializeWorker()
        {
            IBackgroundWorkerFactory factory = new BackgroundWorkerFactory();

            return factory.GetWorker(BackgroundWorkerType.Sync);
        }

        private class WorkerTesterResult
        {
            public bool IsDoWorkCalled { get; set; }
            public object DoWorkArgument { get; set; }
            public bool IsBusy { get; set; }
            public bool CancellationPending { get; set; }
            public bool IsRunWorkerCompletedCalled { get; set; }
            public RunWorkerCompletedEventArgs OnRunWorkerComplatedEventArgs { get; set; }
        }

        private class WorkerTester
        {
            IBackgroundWorker worker;

            public WorkerTester(IBackgroundWorker worker)
            {
                this.worker = worker;
            }

            public WorkerTesterResult RunWorkerAsync()
            {
                WorkerTesterResult result = new WorkerTesterResult();

                worker.OnDoWork += (sender, e) =>
                {
                    result.IsDoWorkCalled = true;
                    result.DoWorkArgument = e.Argument;
                    result.IsBusy = worker.IsBusy;
                    result.CancellationPending = worker.CancellationPending;
                };

                worker.OnRunWorkerCompleted += (sender, e) =>
                {
                    result.IsRunWorkerCompletedCalled = true;
                };

                worker.RunWorkerAsync();

                return result;
            }

            public WorkerTesterResult RunWorkerAsync(object argument)
            {
                WorkerTesterResult result = new WorkerTesterResult();

                worker.OnDoWork += (sender, e) =>
                {
                    result.IsDoWorkCalled = true;
                    result.DoWorkArgument = e.Argument;
                    result.IsBusy = worker.IsBusy;
                    result.CancellationPending = worker.CancellationPending;
                };

                worker.OnRunWorkerCompleted += (sender, e) =>
                {
                    result.IsRunWorkerCompletedCalled = true;
                };

                worker.RunWorkerAsync(argument);

                return result;
            }

            public WorkerTesterResult RunWorkerAsyncWithException(object argument)
            {
                WorkerTesterResult result = new WorkerTesterResult();

                worker.OnDoWork += (sender, e) =>
                {
                    throw new Exception("OnDoWork exception");
                };

                worker.OnRunWorkerCompleted += (sender, e) =>
                {
                    result.OnRunWorkerComplatedEventArgs = e;
                };

                worker.RunWorkerAsync(argument);

                return result;
            }

            public WorkerTesterResult RunWorkerAsyncWithCancel(object argument)
            {
                WorkerTesterResult result = new WorkerTesterResult();

                worker.OnDoWork += (sender, e) =>
                {
                    e.Cancel = true;
                };

                worker.OnRunWorkerCompleted += (sender, e) =>
                {
                    result.OnRunWorkerComplatedEventArgs = e;
                };

                worker.RunWorkerAsync(argument);

                return result;
            }
        }

        [Test]
        public void RunWorkerAsync_SubscribeToOnDoWorkEventAndRunWorkerWithoutArgument_InvokesOnDoWorkEvent()
        {
            // arrange
            WorkerTester tester = new WorkerTester(InitializeWorker());

            // act
            WorkerTesterResult result = tester.RunWorkerAsync();

            // assert
            Assert.IsTrue(result.IsDoWorkCalled);
        }

        [TestCase(null)]
        [TestCase(11)]
        [TestCase("result")]
        public void RunWorkerAsync_SubscribeToOnDoWorkEventAndRunWorkerWithArgument_InvokesOnDoWorkEventWithSuppliedArgument(object expectedArgument)
        {
            // arrange
            WorkerTester tester = new WorkerTester(InitializeWorker());

            // act
            WorkerTesterResult result = tester.RunWorkerAsync(expectedArgument);

            // assert
            Assert.AreEqual(expectedArgument, result.DoWorkArgument);
        }

        [Test]
        public void RunWorkerAsync_SubscribeToOnDoWorkEvent_WhenEventIsFiredWorkerIsBusy()
        {
            // arrange
            WorkerTester tester = new WorkerTester(InitializeWorker());

            // act
            WorkerTesterResult result = tester.RunWorkerAsync();

            // assert
            Assert.IsTrue(result.IsBusy);
        }

        [Test]
        public void RunWorkerAsync_SubscribeToOnDoWorkEventAndRunWorker_SetsCancellationPendingToFalse()
        {
            // arrange
            WorkerTester tester = new WorkerTester(InitializeWorker());

            // act
            WorkerTesterResult result = tester.RunWorkerAsync();

            // assert
            Assert.IsFalse(result.CancellationPending);
        }

        [Test]
        public void RunWorkerAsync_SubscribeToOnRunWorkerCompletedEventAndRunWorkerWithoutArgument_InvokesOnRunWorkerCompletedWhenTaskFinishes()
        {
            // arrange
            WorkerTester tester = new WorkerTester(InitializeWorker());

            // act
            WorkerTesterResult result = tester.RunWorkerAsync();

            // assert
            Assert.IsTrue(result.IsRunWorkerCompletedCalled);
        }

        [Test]
        public void RunWorkerAsync_SubscribeToOnRunWorkerCompletedEventAndWorkThrowsException_InvokesOnRunWorkerCompletedWithExceptionDetails()
        {
            // arrange
            WorkerTester tester = new WorkerTester(InitializeWorker());

            // act
            WorkerTesterResult result = tester.RunWorkerAsyncWithException(null);

            // assert
            Assert.That(result.OnRunWorkerComplatedEventArgs.Error, Is.InstanceOf<Exception>());
        }

        [Test]
        public void RunWorkerAsync_SubscribeToOnRunWorkerCompletedEventAndWorkCancelsOperation_InvokesOnRunWorkerCompletedWithCancelSetToTrue()
        {
            // arrange
            WorkerTester tester = new WorkerTester(InitializeWorker());

            // act
            WorkerTesterResult result = tester.RunWorkerAsyncWithCancel(null);

            // assert
            Assert.IsTrue(result.OnRunWorkerComplatedEventArgs.Cancelled);
        }

        [Test]
        public void CancelAsync_RequestOperationCancellationWhileWorkerSupportsCancellationIsFalse_ThrowsInvalidOperationException()
        {
            // arrange
            IBackgroundWorker worker = InitializeWorker();

            TestDelegate testDelegate = () => worker.CancelAsync();

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void CancelAsync_RequestOperationCancellation_SetsCancellationPendingPropertyToTrue()
        {
            // arrange
            IBackgroundWorker worker = InitializeWorker();

            worker.WorkerSupportsCancellation = true;

            // act
            worker.CancelAsync();

            // assert
            Assert.IsTrue(worker.CancellationPending);
        }

        [Test]
        public void ReportProgress_InvokeFunctionWhileWorkerReportsProgressIsFalse_ThrowsInvalidOperationException()
        {
            // arrange
            IBackgroundWorker worker = InitializeWorker();

            TestDelegate testDelegate = () => worker.ReportProgress(It.IsAny<int>());

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void ReportProgress_SubscribeToOnProgressChangedEventAndReportProgressWithoutUserState_ReportsExpectedProgress()
        {
            // arrange
            IBackgroundWorker worker = InitializeWorker();

            worker.WorkerReportsProgress = true;

            int expectedPercentage = 10;

            int resultPercentage = 0;

            worker.OnProgressChanged += (sender, e) =>
            {
                resultPercentage = e.ProgressPercentage;
            };

            // act
            worker.ReportProgress(expectedPercentage);

            // assert
            Assert.AreEqual(expectedPercentage, resultPercentage);
        }

        [Test]
        public void ReportProgress_SubscribeToOnProgressChangedEventAndReportProgressWithUserState_ReportsExpectedProgressWithUserState()
        {
            // arrange
            IBackgroundWorker worker = InitializeWorker();

            worker.WorkerReportsProgress = true;

            int expectedPercentage = 10;

            object expectedUserState = "user state";

            object resultUserState = "";

            worker.OnProgressChanged += (sender, e) =>
            {
                resultUserState = e.UserState;
            };

            // act
            worker.ReportProgress(expectedPercentage, expectedUserState);

            // assert
            Assert.AreEqual(expectedUserState, resultUserState);
        }

        //public void RunWorkerAsync_StartsExecutionWhenAnotherOperationIsInProgress_ThrowsInvalidOperationException()
        //{
        //    // arrange

        //    // act

        //    // assert
        //}

        //[Test]
        //public void RunWorkerAsync_TryToRunWorkerAgainWhileWorkIsInProgress_ThrowsInvalidOperationException()
        //{
        //    // arrange
        //    IBackgroundWorker worker = InitializeWorker();

        //    worker.OnDoWork += (sender, e) =>
        //    {
        //        worker.RunWorkerAsync();
        //    };

        //    TestDelegate testDelegate = () => worker.RunWorkerAsync();

        //    // act, assert
        //    Assert.That(testDelegate, Throws.Exception.TypeOf<InvalidOperationException>());
        //}
    }
}