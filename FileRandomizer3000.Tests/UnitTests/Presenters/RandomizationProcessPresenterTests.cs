using FileRandomizer3000.Core.Infrastructure.Interfaces;
using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Presenters;
using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Core.Views;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileRandomizer3000.Tests.UnitTests.Presenters
{
    public class RandomizationProcessPresenterTests
    {
        private Mock<IRandomizerWorker> _randomizerWorker = null;
        private Mock<IRandomizationProcessView> _viewMock = null;
        private Mock<IApplicationController> _controllerMock = null;
        private Mock<RandomizationProcessViewModel> _viewModelMock = null;
        private RandomizationProcessPresenter _presenter = null;

        private void SetUp(bool createPresenter = true, bool executeRun = true)
        {
            _controllerMock = new Mock<IApplicationController>();
            _viewMock = new Mock<IRandomizationProcessView>();
            _randomizerWorker = new Mock<IRandomizerWorker>();
            _viewModelMock = new Mock<RandomizationProcessViewModel>(new Mock<GlobalWizardViewModel>(It.IsAny<string>()).Object);

            if (createPresenter)
            {
                _presenter = new RandomizationProcessPresenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, _randomizerWorker.Object);

                if (executeRun)
                {
                    _presenter.Run();
                }
            }
        }

        [Test]
        public void Constructor_SupplyNullModel_ThrowsArgumentNullException()
        {
            SetUp(false);

            TestDelegate testDelegate = () => new RandomizationProcessPresenter(_controllerMock.Object, _viewMock.Object, null, null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("model"));
        }

        [Test]
        public void Constructor_SupplyNullRandomizerWorker_ThrowsArgumentNullException()
        {
            SetUp(false);

            TestDelegate testDelegate = () => new RandomizationProcessPresenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("randomizerWorker"));
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
        public void RaiseOnBeforeViewShown_CallsRandomizerWorkerRun()
        {
            SetUp();

            _randomizerWorker.Setup(x => x.Run(_viewModelMock.Object.Settings, _presenter.OnFinished, _presenter.OnFailed, _presenter.OnCancelled)).Verifiable();

            _viewMock.Raise(x => x.OnBeforeViewShown += null);

            _randomizerWorker.Verify();
        }

        [Test]
        public void RaiseOnBeforeViewShown_CallsViewStartRandomization()
        {
            SetUp();

            _viewMock.Setup(x => x.StopRandomization()).Verifiable();

            _viewMock.Raise(x => x.OnBeforeViewShown += null);

            _viewMock.Verify(x => x.StartRandomization(), Times.Once);
        }

        [Test]
        public void RaiseOnRandomizationStopped_CallsRandomizerWorkerCancel()
        {
            SetUp();

            _randomizerWorker.Setup(x => x.Cancel());

            _viewMock.Raise(x => x.OnRandomizationStopped += null);

            _randomizerWorker.Verify(x => x.Cancel(), Times.Once);
        }

        [Test]
        public void RaiseOnRandomizationStopped_CallsViewOnRandomizationStopped()
        {
            SetUp();

            _viewMock.Setup(x => x.StopRandomization()).Verifiable();

            _viewMock.Raise(x => x.OnRandomizationStopped += null);

            _viewMock.Verify(x => x.StopRandomization(), Times.Once);
        }

        [Test]
        public void RaiseOnRandomizationAborted_CallsStep3Presenter()
        {
            SetUp();
            _controllerMock.Setup(x => x.RunSingleton<Step3Presenter>());

            _viewMock.Raise(x => x.OnRandomizationAborted += null);

            _controllerMock.Verify(x => x.RunSingleton<Step3Presenter>(), Times.Once);
        }

        [Test]
        public void OnFinished_FilesListIsNull_DoesNotCallStep5Presenter()
        {
            SetUp();

            List<AppFile> files = null;

            _controllerMock.Setup(x => x.Run<CopyProcessPresenter, List<AppFile>>(files));

            _presenter.OnFinished(files);

            _controllerMock.Verify(x => x.Run<CopyProcessPresenter, List<AppFile>>(files), Times.Never);
        }

        [Test]
        public void OnFinished_FilesListIsNull_CallsViewNothingToCopy()
        {
            SetUp();

            List<AppFile> files = new List<AppFile>();

            _viewMock.Setup(x => x.NothingToCopy());

            _presenter.OnFinished(files);

            _viewMock.Verify(x => x.NothingToCopy(), Times.Once);
        }

        [Test]
        public void OnFinished_FilesListIsEmpty_DoesNotCallStep5Presenter()
        {
            SetUp();

            List<AppFile> files = new List<AppFile>();

            _controllerMock.Setup(x => x.Run<CopyProcessPresenter, List<AppFile>>(files));

            _presenter.OnFinished(files);

            _controllerMock.Verify(x => x.Run<CopyProcessPresenter, List<AppFile>>(files), Times.Never);
        }

        [Test]
        public void OnFinished_FilesListIsEmpty_CallsViewNothingToCopy()
        {
            SetUp();

            List<AppFile> files = new List<AppFile>();

            _viewMock.Setup(x => x.NothingToCopy());

            _presenter.OnFinished(files);

            _viewMock.Verify(x => x.NothingToCopy(), Times.Once);
        }

        [Test]
        public void OnFinished_FilesListContainsFiles_CallsStep5PresenterWithFiles()
        {
            SetUp();

            List<AppFile> files = new List<AppFile> { It.IsAny<AppFile>() };

            _controllerMock.Setup(x => x.Run<CopyProcessPresenter, List<AppFile>>(files));

            _presenter.OnFinished(files);

            _controllerMock.Verify(x => x.Run<CopyProcessPresenter, List<AppFile>>(files), Times.Once);
        }

        [Test]
        public void OnFailed_CallsViewAlertErrorMessage()
        {
            SetUp();

            string message = "xxx";

            _viewMock.Setup(x => x.AlertErrorMessage(message));

            _presenter.OnFailed(message);

            _viewMock.Verify(x => x.AlertErrorMessage(message), Times.Once);
        }

        [Test]
        public void OnCancelled_CallsViewRandomizationAborted()
        {
            SetUp();

            _viewMock.Setup(x => x.RandomizationAborted());

            _presenter.OnCancelled();

            _viewMock.Verify(x => x.RandomizationAborted(), Times.Once);
        }
    }
}
