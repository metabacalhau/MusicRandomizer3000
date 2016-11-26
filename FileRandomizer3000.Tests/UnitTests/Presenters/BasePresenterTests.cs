using FileRandomizer3000.Core.Infrastructure.Interfaces;
using FileRandomizer3000.Core.Presenters;
using FileRandomizer3000.Core.Views;
using FileRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileRandomizer3000.Tests.UnitTests.Presenters
{
    public class BasePresenterTests
    {
        private Mock<IApplicationController> _controllerMock = null;
        private Mock<IView> _viewMock = null;

        private class PresenterNoArgument : BasePresenter<IView>
        {
            public PresenterNoArgument(IApplicationController controller, IView view) : base(controller, view) { }
            public new IApplicationController Controller { get { return base.Controller; } }
            public new IView View { get { return base.View; } }
        }

        private class PresenterWithArgument : BasePresenter<IView, object>
        {
            public PresenterWithArgument(IApplicationController controller, IView view) : base(controller, view) { }
            public new IApplicationController Controller { get { return base.Controller; } }
            public new IView View { get { return base.View; } }
            public override void Run(object argument) { }
        }

        [SetUp]
        public void SetUp()
        {
            _controllerMock = new Mock<IApplicationController>();
            _viewMock = new Mock<IView>();
        }

        [Test]
        public void PresenterNoArgument_Constructor_SupplyNullApplicationController_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () =>
            {
                MockMustThrowExceptionHack.ExecuteMock(() =>
                {
                    var target = new Mock<BasePresenter<IView>>(null, _viewMock.Object).Object;
                });
            };

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("controller"));
        }

        [Test]
        public void PresenterNoArgument_Constructor_SupplyNullView_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () =>
            {
                MockMustThrowExceptionHack.ExecuteMock(() =>
                {
                    var target = new Mock<BasePresenter<IView>>(_controllerMock.Object, null).Object;
                });
            };

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("view"));
        }

        [Test]
        public void PresenterNoArgument_Controller_ReturnsSetValue()
        {
            PresenterNoArgument test = new PresenterNoArgument(_controllerMock.Object, _viewMock.Object);

            Assert.AreEqual(_controllerMock.Object, test.Controller);
        }

        [Test]
        public void PresenterNoArgument_View_ReturnsSetValue()
        {
            PresenterNoArgument test = new PresenterNoArgument(_controllerMock.Object, _viewMock.Object);

            Assert.AreEqual(_viewMock.Object, test.View);
        }

        [Test]
        public void PresenterNoArgument_Run_CallsViewShow()
        {
            var mock = new Mock<BasePresenter<IView>>(_controllerMock.Object, _viewMock.Object);
            _viewMock.Setup(x => x.Show());

            mock.Object.Run();

            _viewMock.Verify(x => x.Show(), Times.Once);
        }

        [Test]
        public void PresenterWithArgument_Constructor_SupplyNullApplicationController_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () =>
            {
                MockMustThrowExceptionHack.ExecuteMock(() =>
                {
                    var target = new Mock<BasePresenter<IView, object>>(null, _viewMock.Object).Object;
                });
            };

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("controller"));
        }

        [Test]
        public void PresenterWithArgument_Constructor_SupplyNullView_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () =>
            {
                MockMustThrowExceptionHack.ExecuteMock(() =>
                {
                    var target = new Mock<BasePresenter<IView, object>>(_controllerMock.Object, null).Object;
                });
            };

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("view"));
        }

        [Test]
        public void PresenterWithArgument_Controller_ReturnsSetValue()
        {
            PresenterWithArgument test = new PresenterWithArgument(_controllerMock.Object, _viewMock.Object);

            Assert.AreEqual(_controllerMock.Object, test.Controller);
        }

        [Test]
        public void PresenterWithArgument_View_ReturnsSetValue()
        {
            PresenterWithArgument test = new PresenterWithArgument(_controllerMock.Object, _viewMock.Object);

            Assert.AreEqual(_viewMock.Object, test.View);
        }
    }
}
