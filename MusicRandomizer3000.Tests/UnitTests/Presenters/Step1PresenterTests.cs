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
using System.Linq;

namespace MusicRandomizer3000.Tests.UnitTests.Presenters
{
    [TestFixture]
    public class Step1PresenterTests
    {
        Step1Presenter _presenter = null;
        Mock<IFolderService> _folderServiceMock = null;
        Mock<IStep1View> _viewMock = null;
        Mock<IApplicationController> _controllerMock = null;
        Mock<Step1ViewModel> _viewModelMock = null;

        public void SetUp(bool createPresenter = true, bool executeRun = true)
        {
            _folderServiceMock = new Mock<IFolderService>();
            _viewMock = new Mock<IStep1View>();
            _controllerMock = new Mock<IApplicationController>();
            _viewModelMock = new Mock<Step1ViewModel>(new Mock<GlobalWizardViewModel>(It.IsAny<string>()).Object, new Mock<ISettingsService>().Object);

            if (createPresenter)
            {
                _presenter = new Step1Presenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, _folderServiceMock.Object);

                if (executeRun)
                {
                    _presenter.Run();
                }
            }
        }

        [Test]
        public void Constructor_SupplyNullFolderService_ThrowsArgumentNullException()
        {
            SetUp(false);

            TestDelegate testDelegate = () => new Step1Presenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("folderService"));
        }

        [Test]
        public void Constructor_SupplyNullModel_ThrowsArgumentNullException()
        {
            SetUp(false);

            TestDelegate testDelegate = () => new Step1Presenter(_controllerMock.Object, _viewMock.Object, null, _folderServiceMock.Object);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("model"));
        }

        [Test]
        public void Run_CallsSetActiveView()
        {
            SetUp();

            _viewMock.Setup(x => x.SetActiveView());

            _viewMock.Verify(x => x.SetActiveView(), Times.Once);
        }

        [Test]
        public void RaiseOnBeforeViewShown_ViewIsInitialized_UpdatesViewWithExpectedModel()
        {
            SetUp();

            _viewMock.Setup(x => x.IsInitialized).Returns(true);

            _viewModelMock.SetupSet(x => x.FormTitle = It.IsAny<string>());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);

            _viewModelMock.VerifySet(x => x.FormTitle = It.IsAny<string>(), Times.Once);
        }

        [Test]
        public void RaiseOnBeforeViewShown_ViewIsNotInitialized_InitializesViewWithExpectedModel()
        {
            SetUp();

            _viewMock.Setup(x => x.Initialize(_viewModelMock.Object));

            _viewMock.Raise(x => x.OnBeforeViewShown += null);

            _viewMock.Verify(x => x.Initialize(_viewModelMock.Object), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_PathFromIsNotSelected_CallsShowPathFromIsEmptyErrorAndExpectedError()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathsFrom).Returns((List<string>)null);
            _viewMock.Setup(x => x.ShowPathFromIsEmptyError(It.IsAny<string>()));

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewMock.Verify(x => x.ShowPathFromIsEmptyError(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_PathFromIsNotAccessible_CallsShowPathFromIsEmptyErrorAndExpectedError()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathsFrom).Returns(new List<string> { "sfddsggd" });
            _viewModelMock.Object.PathsFrom.ToList().ForEach(x => _folderServiceMock.Setup(y => y.IsAccessible(x)).Returns(false));
            _viewMock.Setup(x => x.ShowPathFromIsInaccessible(It.IsAny<string>()));

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewMock.Verify(x => x.ShowPathFromIsInaccessible(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsUpdateSettings()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathsFrom).Returns(new List<string> { "sfddsggd" });
            _viewModelMock.Object.PathsFrom.ToList().ForEach(x => _folderServiceMock.Setup(y => y.IsAccessible(x)).Returns(true));
            _viewModelMock.SetupGet(x => x.SaveSettings).Returns(true);
            _viewModelMock.Setup(x => x.UpdateSettings());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewModelMock.Verify(x => x.UpdateSettings(), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsUpdateGlobalModel()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathsFrom).Returns(new List<string> { "sfddsggd" });
            _viewModelMock.Object.PathsFrom.ToList().ForEach(x => _folderServiceMock.Setup(y => y.IsAccessible(x)).Returns(true));
            _viewModelMock.Setup(x => x.UpdateGlobalModel());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewModelMock.Verify(x => x.UpdateGlobalModel(), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsHidePathFromError()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathsFrom).Returns(new List<string> { "sfddsggd" });
            _viewModelMock.Object.PathsFrom.ToList().ForEach(x => _folderServiceMock.Setup(y => y.IsAccessible(x)).Returns(true));
            _viewMock.Setup(x => x.HidePathFromError());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewMock.Verify(x => x.HidePathFromError(), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsHideOrganizeFilesDescription()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathsFrom).Returns(new List<string> { "sfddsggd" });
            _viewModelMock.Object.PathsFrom.ToList().ForEach(x => _folderServiceMock.Setup(y => y.IsAccessible(x)).Returns(true));
            _viewMock.Setup(x => x.HideOrganizeFilesDescription());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewMock.Verify(x => x.HideOrganizeFilesDescription(), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsStep2Presenter()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathsFrom).Returns(new List<string> { "sfddsggd" });
            _viewModelMock.Object.PathsFrom.ToList().ForEach(x => _folderServiceMock.Setup(y => y.IsAccessible(x)).Returns(true));
            _controllerMock.Setup(x => x.RunSingleton<Step2Presenter>());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _controllerMock.Verify(x => x.RunSingleton<Step2Presenter>(), Times.Once);
        }

        [Test]
        public void RaiseOnOrganizeFilesDescriptionClick_CallsShowOrganizeFilesDescriptionWithExpectedString()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.FilesAndFolders).Returns(new FilesAndFoldersLimitSettings());
            _viewMock.Setup(x => x.ShowOrganizeFilesDescription(It.IsAny<string>())).Verifiable();

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnOrganizeFilesDescriptionClick += null);

            _viewMock.Verify(x => x.ShowOrganizeFilesDescription(It.IsAny<string>()), Times.Once);
        }
    }
}