using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Core.Views;
using FileRandomizer3000.WinForms.Views;
using Moq;
using NUnit.Framework;
using System;

namespace FileRandomizer3000.Tests.UnitTests.Views
{
    [TestFixture]
    public class Step1ViewTests
    {
        Mock<IMainFormViewHost> viewHostMock = null;
        Mock<Step1ViewModel> viewModelMock = null;
        Mock<ISettingsService> settingsServiceMock = null;
        Mock<GlobalWizardViewModel> globalViewModelMock = null;
        Step1View view = null;

        [SetUp]
        public void SetUp()
        {
            viewHostMock = new Mock<IMainFormViewHost>();
            globalViewModelMock = new Mock<GlobalWizardViewModel>(It.IsAny<string>());
            settingsServiceMock = new Mock<ISettingsService>();
            viewModelMock = new Mock<Step1ViewModel>(globalViewModelMock.Object, settingsServiceMock.Object);
            viewModelMock.CallBase = true;
            view = new Step1View(viewHostMock.Object);
        }

        [Test]
        public void Constructor_SupplyNullViewHost_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new Step1View(null);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("viewHost"));
        }

        [Test]
        public void Initialize_SupplyNullViewModel_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => view.Initialize(null);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("model"));
        }

        [Test]
        public void Initialize_ViewIsInitialized()
        {
            view.Initialize(viewModelMock.Object);

            Assert.IsTrue(view.IsInitialized);
        }

        [Test]
        public void Show_MakesViewVisible()
        {
            ((IView)view).Show();

            Assert.IsTrue(view.Visible);
        }

        [Test]
        public void RaiseOnBeforeViewShownEvent_EventIsRaisedInShowMethod_EventFires()
        {
            bool handlerFired = false;

            view.OnBeforeViewShown += () => handlerFired = true;

            ((IView)view).Show();

            Assert.IsTrue(handlerFired);
        }

        [Test]
        public void Close_MakesViewInvisible()
        {
            ((IView)view).Close();

            Assert.IsFalse(view.Visible);
        }

        [Test]
        public void SetActiveView_MakesViewActiveOnViewHost()
        {
            viewHostMock.Setup(x => x.ActivateView(view));

            view.SetActiveView();

            viewHostMock.VerifyAll();
        }
    }
}