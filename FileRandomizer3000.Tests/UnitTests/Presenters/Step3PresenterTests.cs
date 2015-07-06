using FileRandomizer3000.Core.Infrastructure.Interfaces;
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
    public class Step3PresenterTests
    {
        private Step3Presenter _presenter = null;
        private Mock<IApplicationController> _controllerMock = null;
        private Mock<IStep3View> _viewMock = null;
        private Mock<Step3ViewModel> _viewModelMock = null;

        private void SetUp(bool createPresenter = true, bool executeRun = true)
        {
            _viewMock = new Mock<IStep3View>();
            _controllerMock = new Mock<IApplicationController>();
            _viewModelMock = new Mock<Step3ViewModel>(new Mock<GlobalWizardViewModel>(It.IsAny<string>()).Object, new Mock<ISettingsService>().Object);

            if (createPresenter)
            {
                _presenter = new Step3Presenter(_controllerMock.Object, _viewMock.Object, _viewModelMock.Object);

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

            TestDelegate testDelegate = () => new Step3Presenter(_controllerMock.Object, _viewMock.Object, null);

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
        public void RaiseOnNextStepClick_CallsUpdateSettings()
        {
            SetUp();
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
            _viewModelMock.Setup(x => x.UpdateGlobalModel());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _viewModelMock.Verify(x => x.UpdateGlobalModel(), Times.Once);
        }

        [Test]
        public void RaiseOnNextStepClick_CallsRandomizationProcessPresenter()
        {
            SetUp();
            _controllerMock.Setup(x => x.Run<RandomizationProcessPresenter>());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnNextStepClick += null);

            _controllerMock.Verify(x => x.Run<RandomizationProcessPresenter>(), Times.Once);
        }

        [Test]
        public void RaiseOnPreviousStepClick_CallsStep2Presenter()
        {
            SetUp();
            _controllerMock.Setup(x => x.RunSingleton<Step2Presenter>());

            _viewMock.Raise(x => x.OnBeforeViewShown += null);
            _viewMock.Raise(x => x.OnPreviousStepClick += null);

            _controllerMock.Verify(x => x.RunSingleton<Step2Presenter>(), Times.Once);
        }
    }
}
