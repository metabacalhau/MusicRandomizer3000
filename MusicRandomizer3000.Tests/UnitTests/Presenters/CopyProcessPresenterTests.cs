using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using MusicRandomizer3000.Core.Models;
using MusicRandomizer3000.Core.Presenters;
using MusicRandomizer3000.Core.Services.Interfaces;
using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Core.Views;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MusicRandomizer3000.Tests.UnitTests.Presenters
{
    public class CopyProcessPresenterTests
    {
        private Mock<IApplicationController> _controllerMock = null;
        private Mock<ICopyProcessView> _viewMock = null;
        private Mock<CopyProcessViewModel> _viewModelMock = null;
        private Mock<ICopyWorker> _copyWorker = null;
        private Mock<List<AppFile>> _filesToCopyMock = null;
        private Mock<IProcessWrapper> _processWrapper = null;
        private CopyProcessPresenter _presenter = null;

        private void SetUp(bool createPresenter = true, bool executeRun = true)
        {
            _controllerMock = new Mock<IApplicationController>();
            _viewMock = new Mock<ICopyProcessView>();
            _viewModelMock = new Mock<CopyProcessViewModel>(new Mock<GlobalWizardViewModel>(It.IsAny<string>()).Object);
            _filesToCopyMock = new Mock<List<AppFile>>();
            _copyWorker = new Mock<ICopyWorker>();
            _processWrapper = new Mock<IProcessWrapper>();

            if (createPresenter)
            {
                _presenter = new CopyProcessPresenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, _copyWorker.Object, _processWrapper.Object);

                if (executeRun)
                {
                    _viewModelMock.SetupGet(x => x.Settings).Returns(new CopyWorkerSettings());

                    _presenter.Run(_filesToCopyMock.Object);
                }
            }
        }

        [Test]
        public void Constructor_SupplyNullModel_ThrowsArgumentNullException()
        {
            SetUp(false);

            TestDelegate testDelegate = () => new CopyProcessPresenter(_controllerMock.Object, _viewMock.Object, null, null, null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("model"));
        }

        [Test]
        public void Constructor_SupplyNullRandomizerWorker_ThrowsArgumentNullException()
        {
            SetUp(false);

            TestDelegate testDelegate = () => new CopyProcessPresenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, null, null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("copyWorker"));
        }

        [Test]
        public void Constructor_SupplyNullProcessWrapper_ThrowsArgumentNullException()
        {
            SetUp(false);

            TestDelegate testDelegate = () => new CopyProcessPresenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, _copyWorker.Object, null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("processWrapper"));
        }

        [Test]
        public void Run_CallsViewInitialize()
        {
            SetUp();

            _viewMock.Setup(x => x.Initialize(_viewModelMock.Object));

            _viewMock.Verify(x => x.Initialize(_viewModelMock.Object), Times.Once);
        }

        [Test]
        public void Run_CallsSetActiveView()
        {
            SetUp();

            _viewMock.Setup(x => x.SetActiveView());

            _viewMock.Verify(x => x.SetActiveView(), Times.Once);
        }

        [Test]
        public void RaiseOnBeforeViewShown_CallsCopyWorkerRun()
        {
            SetUp();

            _copyWorker.Setup(x => x.Run(It.IsAny<CopyWorkerParameters>())).Verifiable();

            _viewMock.Raise(x => x.OnBeforeViewShown += null);

            _copyWorker.Verify();
        }

        [Test]
        public void RaiseOnBeforeViewShown_CallsViewStartCopy()
        {
            SetUp();

            _viewMock.Setup(x => x.StartCopy()).Verifiable();

            _viewMock.Raise(x => x.OnBeforeViewShown += null);

            _viewMock.Verify();
        }

        [Test]
        public void RaiseOnCopyStopped_CallsCopyWorkerCancel()
        {
            SetUp();

            _copyWorker.Setup(x => x.Cancel());

            _viewMock.Raise(x => x.OnCopyStopped += null);

            _copyWorker.Verify();
        }

        [Test]
        public void RaiseOnCopyStopped_CallsViewStopCopy()
        {
            SetUp();

            _viewMock.Setup(x => x.StopCopy());

            _viewMock.Raise(x => x.OnCopyStopped += null);

            _viewMock.Verify();
        }

        [Test]
        public void OnStarted_CallsViewCopyStarted()
        {
            SetUp();

            _viewMock.Setup(x => x.CopyStarted());

            _presenter.OnStarted();

            _viewMock.Verify();
        }

        [Test]
        public void OnFinished_CallsViewCopyFinished()
        {
            SetUp();

            _viewMock.Setup(x => x.CopyFinished());

            _presenter.OnFinished();

            _viewMock.Verify();
        }

        [Test]
        public void OnFailed_CallsViewAlertErrorMessageWithExpectedMessage()
        {
            SetUp();

            string message = "test";

            _viewMock.Setup(x => x.AlertErrorMessage(message));

            _presenter.OnFailed(message);

            _viewMock.Verify();
        }

        [Test]
        public void OnCancelled_CallsViewCopyAborted()
        {
            SetUp();

            _viewMock.Setup(x => x.CopyAborted());

            _presenter.OnCancelled();

            _viewMock.Verify();
        }

        [Test]
        public void OnFileChanged_SetsFileFullNameOnModelProgressText()
        {
            SetUp();

            AppFile file = new AppFile
            {
                FileNameFull = "test"
            };

            _viewModelMock
                .SetupSet(x => x.ProgressInfoText = It.IsAny<string>())
                .Callback<string>(x => Assert.IsTrue(x == file.FileNameFull));

            _presenter.OnFileChanged(file);

            _viewMock.Verify();
        }

        [Test]
        public void OnProgressChanged_CallsViewReportProgressWithExpectedArgument()
        {
            SetUp();

            int progressPercentage = 10;

            _viewMock.Setup(x => x.ReportProgress(progressPercentage));

            _presenter.OnProgressChanged(progressPercentage);

            _viewMock.Verify();
        }
    }
}
