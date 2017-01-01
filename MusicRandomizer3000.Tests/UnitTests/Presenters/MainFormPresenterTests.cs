using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using MusicRandomizer3000.Core.Presenters;
using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Core.Views;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MusicRandomizer3000.Tests.UnitTests.Presenters
{
    public class MainFormPresenterTests
    {
        Mock<IApplicationController> _controller = null;
        Mock<IMainFormView> _view = null;
        Mock<GlobalWizardViewModel> _model = null;
        MainFormPresenter _presenter = null;

        private class TestView : IView
        {
            public void Show() { }
            public void Close() { }
        }

        [SetUp]
        public void Initialization()
        {
            _controller = new Mock<IApplicationController>();
            _view = new Mock<IMainFormView>();
            _model = new Mock<GlobalWizardViewModel>(It.IsAny<string>());
            _presenter = new MainFormPresenter(_controller.Object, _view.Object, _model.Object);
        }

        [Test]
        public void Constructor_SupplyInvalidGlobalWizardViewModel_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new MainFormPresenter(_controller.Object, _view.Object, null);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("model"));
        }

        [Test]
        public void Run_InitializedStep1Presenter()
        {
            _controller.Setup(x => x.RunSingleton<Step1Presenter>());

            _presenter.Run();

            _controller.Verify(x => x.RunSingleton<Step1Presenter>(), Times.Once);
        }

        [Test]
        public void Run_InitializesView()
        {
            _view.Setup(x => x.Initialize(_model.Object));

            _presenter.Run();

            _view.Verify(x => x.Initialize(_model.Object), Times.Once);
        }

        [Test]
        public void OnActiveViewChanged_SupplyNullNewActiveView_ThrowsAgumentNullException()
        {
            _presenter.Run();

            TestDelegate testDelegate = () => _view.Raise(x => x.OnActiveViewChanged += null, null, null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("newActiveView"));
        }

        [Test]
        public void OnActiveViewChanged_SupplyNonExistingView_CallsAddView()
        {
            _presenter.Run();

            IView viewMock = new Mock<IView>().Object;

            _view.Setup(x => x.AddView(viewMock));

            _view.Raise(x => x.OnActiveViewChanged += null, null, viewMock);

            _view.Verify(x => x.AddView(viewMock), Times.Once);
        }

        [Test]
        public void OnActiveViewChanged_SupplyViewOfNonExistingType_CallsAddView()
        {
            _presenter.Run();

            IView viewFake = new TestView(); ;

            _view.Setup(x => x.Views).Returns(new ReadOnlyCollection<IView>(new List<IView>() { new Mock<IView>().Object }));
            _view.Setup(x => x.AddView(viewFake));

            _view.Raise(x => x.OnActiveViewChanged += null, null, viewFake);

            _view.Verify(x => x.AddView(viewFake), Times.Once);
        }

        [Test]
        public void OnActiveViewChanged_SupplyNewActiveView_CallsShowOnIt()
        {
            _presenter.Run();

            Mock<IView> viewMock = new Mock<IView>();

            viewMock.Setup(x => x.Show());

            _view.Raise(x => x.OnActiveViewChanged += null, null, viewMock.Object);

            viewMock.Verify(x => x.Show(), Times.Once);
        }

        [Test]
        public void OnActiveViewChanged_SupplyOldAndNewActiveView_CallsCloseOnOldActiveView()
        {
            _presenter.Run();

            Mock<IView> oldActiveViewMock = new Mock<IView>();
            Mock<IView> newActiveViewMock = new Mock<IView>();

            oldActiveViewMock.Setup(x => x.Close());

            _view.Raise(x => x.OnActiveViewChanged += null, oldActiveViewMock.Object, newActiveViewMock.Object);

            oldActiveViewMock.Verify(x => x.Close(), Times.Once);
        }
    }
}