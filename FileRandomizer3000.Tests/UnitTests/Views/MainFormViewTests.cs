using FileRandomizer3000.Core.Infrastructure.Interfaces;
using FileRandomizer3000.Core.Views;
using FileRandomizer3000.WinForms.Views;
using Moq;
using NUnit.Framework;
using System;
using System.Windows.Forms;

namespace FileRandomizer3000.Tests.UnitTests.Views
{
    [TestFixture]
    public class MainFormViewTests
    {
        Mock<ApplicationContext> applicationContextMock = null;
        Mock<IApplicationController> controllerMock = null;
        MainForm mainForm = null;

        private class ViewStub : Control, IView
        {
            public void Close()
            {
                throw new NotImplementedException();
            }
        }

        [SetUp]
        public void Initialize()
        {
            applicationContextMock = new Mock<ApplicationContext>();
            controllerMock = new Mock<IApplicationController>();
            mainForm = new MainForm(applicationContextMock.Object);
        }

        [Test]
        public void AddView_ViewIsAddedToViewsCollection()
        {
            mainForm.AddView(new ViewStub());

            Assert.AreEqual(1, mainForm.Views.Count);
        }

        [Test]
        public void AddView_SupplyNullView_ThrowsArgumenNullException()
        {
            TestDelegate testDelegate = () => mainForm.AddView(null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("view"));
        }

        [Test]
        public void AddView_SupplyNonControlView_ThrowsArgumentException()
        {
            IView fakeView = new Mock<IView>().Object;

            TestDelegate testDelegate = () => mainForm.AddView(fakeView);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void AddView_AddDuplicateView_ThrowsException()
        {
            ViewStub view = new ViewStub();

            TestDelegate testDelegate = () =>
            {
                mainForm.AddView(view);
                mainForm.AddView(view);
            };

            Assert.That(testDelegate, Throws.Exception.TypeOf<Exception>());
        }

        [Test]
        public void RemoveView_SupplyNonControlView_ThrowsArgumentException()
        {
            IView fakeView = new Mock<IView>().Object;

            TestDelegate testDelegate = () => mainForm.RemoveView(fakeView);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void RemoveView_SupplyNullView_ThrowsException()
        {
            TestDelegate testDelegate = () => mainForm.RemoveView(null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("view"));
        }

        [Test]
        public void RemoveView_IsRemovedFromViewsCollection()
        {
            ViewStub view = new ViewStub();

            mainForm.AddView(view);
            mainForm.RemoveView(view);

            Assert.AreEqual(0, mainForm.Views.Count);
        }

        [Test]
        public void RemoveView_ViewIsActive_FiresActiveViewChangedEvent()
        {
            bool fired = false;

            IView fakeView = new ViewStub();

            mainForm.AddView(fakeView);

            mainForm.SetActiveView(fakeView);

            mainForm.OnActiveViewChanged += delegate { fired = true; };

            mainForm.RemoveView(fakeView);

            Assert.IsTrue(fired);
        }

        [Test]
        public void SetActiveView_SupplyNullView_ThrowsException()
        {
            TestDelegate testDelegate = () => mainForm.SetActiveView(null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("view"));
        }

        [Test]
        public void SetActiveView_FiresActiveViewChangedEvent()
        {
            bool fired = false;

            mainForm.OnActiveViewChanged += delegate { fired = true; };

            mainForm.SetActiveView(new ViewStub());

            Assert.IsTrue(fired);
        }

        [Test]
        public void SetActiveView_FiresActiveViewChangedEvent_FormerAndCurrentActiveViewsAreOfExpectedTypes()
        {
            ViewStub expectedView = new ViewStub();

            IView actualView = null;

            mainForm.OnActiveViewChanged += (oldActiveView, newActiveView) =>
            {
                actualView = newActiveView;
            };

            mainForm.SetActiveView(expectedView);

            Assert.AreEqual(expectedView, actualView);
        }

        [Test]
        public void SetActiveView_IsExpectedActiveView()
        {
            ViewStub view = new ViewStub();

            mainForm.AddView(view);
            mainForm.SetActiveView(view);

            Assert.AreEqual(view, mainForm.ActiveView);
        }

        [Test]
        public void RemoveView_ViewIsActive_MakesActiveViewNull()
        {
            ViewStub view = new ViewStub();

            mainForm.AddView(view);
            mainForm.SetActiveView(view);
            mainForm.RemoveView(view);

            Assert.IsNull(mainForm.ActiveView);
        }
    }
}