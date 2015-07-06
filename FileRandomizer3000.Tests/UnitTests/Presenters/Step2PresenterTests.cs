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
    public class Step2PresenterTests
    {
        private Step2Presenter _presenter = null;
        private Mock<IFolderService> _folderServiceMock = null;
        private Mock<IStep2View> _viewMock = null;
        private Mock<IApplicationController> _controllerMock = null;
        private Mock<Step2ViewModel> _viewModelMock = null;

        private void SetUp(bool createPresenter = true, bool executeRun = true)
        {
            _folderServiceMock = new Mock<IFolderService>();
            _viewMock = new Mock<IStep2View>();
            _controllerMock = new Mock<IApplicationController>();
            _viewModelMock = new Mock<Step2ViewModel>(new Mock<GlobalWizardViewModel>(It.IsAny<string>()).Object, new Mock<ISettingsService>().Object);

            if (createPresenter)
            {
                _presenter = new Step2Presenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, _folderServiceMock.Object);

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

            TestDelegate testDelegate = () => new Step2Presenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object, null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("folderService"));
        }

        [Test]
        public void Constructor_SupplyNullModel_ThrowsArgumentNullException()
        {
            SetUp(false);

            TestDelegate testDelegate = () => new Step2Presenter(_controllerMock.Object, _viewMock.Object, null, _folderServiceMock.Object);

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
            _viewModelMock.SetupGet(x => x.PathTo).Returns("");
            _viewMock.Setup(x => x.ShowPathToIsEmptyError("Pick a folder"));

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewMock.Verify(x => x.ShowPathToIsEmptyError("Pick a folder"), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_PathFromIsNotAccessible_CallsShowPathFromIsEmptyErrorAndExpectedError()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathTo).Returns("sfddsggd");
            _folderServiceMock.Setup(x => x.IsAccessible(_viewModelMock.Object.PathTo)).Returns(false);
            _viewMock.Setup(x => x.ShowPathToIsInaccessible("Selected folder is inaccessible"));

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewMock.Verify(x => x.ShowPathToIsInaccessible("Selected folder is inaccessible"), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsUpdateSettings()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathTo).Returns("sfddsggd");
            _folderServiceMock.Setup(x => x.IsAccessible(_viewModelMock.Object.PathTo)).Returns(true);
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
            _viewModelMock.SetupGet(x => x.PathTo).Returns("sfddsggd");
            _folderServiceMock.Setup(x => x.IsAccessible(_viewModelMock.Object.PathTo)).Returns(true);
            _viewModelMock.Setup(x => x.UpdateGlobalModel());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewModelMock.Verify(x => x.UpdateGlobalModel(), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsHidePathToError()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathTo).Returns("sfddsggd");
            _folderServiceMock.Setup(x => x.IsAccessible(_viewModelMock.Object.PathTo)).Returns(true);
            _viewMock.Setup(x => x.HidePathToError());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewMock.Verify(x => x.HidePathToError(), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsHideOnCopyAddDescription()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathTo).Returns("sfddsggd");
            _folderServiceMock.Setup(x => x.IsAccessible(_viewModelMock.Object.PathTo)).Returns(true);
            _viewMock.Setup(x => x.HideOnCopyAddDescription());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewMock.Verify(x => x.HideOnCopyAddDescription(), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsStep3Presenter()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.PathTo).Returns("sfddsggd");
            _folderServiceMock.Setup(x => x.IsAccessible(_viewModelMock.Object.PathTo)).Returns(true);
            _controllerMock.Setup(x => x.RunSingleton<Step3Presenter>());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _controllerMock.Verify(x => x.RunSingleton<Step3Presenter>(), Times.Once);
        }

        [Test]
        public void RaiseOnCopyActionsDescriptionClick_SelectedOnCopyOptionIsNull_DoesNotCallShowOnCopyAddDescription()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.SelectedOnCopyOption).Returns((ComboBoxOption)null);
            _viewMock.Setup(x => x.ShowOnCopyAddDescription(It.IsAny<string>()));

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnCopyActionsDescriptionClick += null);

            _viewMock.Verify(x => x.ShowOnCopyAddDescription(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void RaiseOnCopyActionsDescriptionClick_SelectedOnCopyOptionDescriptionIsNullOrEmpty_DoesNotCallShowOnCopyAddDescription()
        {
            SetUp();
            _viewModelMock.SetupGet(x => x.SelectedOnCopyOption).Returns(new ComboBoxOption { Description = null });
            _viewMock.Setup(x => x.ShowOnCopyAddDescription(It.IsAny<string>()));

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnCopyActionsDescriptionClick += null);

            _viewMock.Verify(x => x.ShowOnCopyAddDescription(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void RaiseOnCopyActionsDescriptionClick_SelectedOnCopyOptionDescriptionIsValid_CallsShowOnCopyAddDescription()
        {
            SetUp();
            ComboBoxOption option = new ComboBoxOption { Description = "dfgdfgdfgfd" };
            _viewModelMock.SetupGet(x => x.SelectedOnCopyOption).Returns(option);
            _viewMock.Setup(x => x.ShowOnCopyAddDescription(option.Description));

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnCopyActionsDescriptionClick += null);

            _viewMock.Verify(x => x.ShowOnCopyAddDescription(option.Description), Times.Once);
        }

        [Test]
        public void RaiseOnPreviousStepClick_CallsHidePathToError()
        {
            SetUp();
            _viewMock.Setup(x => x.HidePathToError());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnPreviousStepClick += null);

            _viewMock.Verify(x => x.HidePathToError(), Times.Once);
        }

        [Test]
        public void RaiseOnPreviousStepClick_CallsHideOnCopyAddDescription()
        {
            SetUp();
            _viewMock.Setup(x => x.HideOnCopyAddDescription());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnPreviousStepClick += null);

            _viewMock.Verify(x => x.HideOnCopyAddDescription(), Times.Once);
        }

        [Test]
        public void RaiseOnPreviousStepClick_CallsStep1Presenter()
        {
            SetUp();
            _controllerMock.Setup(x => x.RunSingleton<Step1Presenter>());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnPreviousStepClick += null);

            _controllerMock.Verify(x => x.RunSingleton<Step1Presenter>(), Times.Once);
        }
    }
}