using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileRandomizer3000.Tests.UnitTests.ViewModels
{
    public class CopyProcessViewModelTests
    {
        private Mock<GlobalWizardViewModel> _globalWizardViewModelMock = null;
        private Mock<CopyProcessViewModel> _step5ViewModelMock = null;

        [SetUp]
        public void SetUp()
        {
            _globalWizardViewModelMock = new Mock<GlobalWizardViewModel>(It.IsAny<string>());
            _step5ViewModelMock = new Mock<CopyProcessViewModel>(_globalWizardViewModelMock.Object);
            _step5ViewModelMock.CallBase = true;
        }

        [Test]
        public void Constructor_SupplyInvalidGlobalWizardViewModel_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new CopyProcessViewModel(null);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("globalModel"));
        }

        [Test]
        public void StopProcessButtonText_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.StopProcessButtonText = "xcsdsddddd";

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.StopProcessButtonText).When(a => a.StopProcessButtonText = "aaa");
        }

        [Test]
        public void StopProcessButtonText_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.StopProcessButtonText = "a";

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.StopProcessButtonText).When(a => a.StopProcessButtonText = "a");
        }

        [Test]
        public void StopProcessButtonText_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.StopProcessButtonText = "111";

            Assert.AreEqual("111", _step5ViewModelMock.Object.StopProcessButtonText);
        }

        [Test]
        public void StopProcessButtonEnabled_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.StopProcessButtonEnabled = false;

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.StopProcessButtonEnabled).When(a => a.StopProcessButtonEnabled = true);
        }

        [Test]
        public void StopProcessButtonEnabled_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.StopProcessButtonEnabled = false;

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.StopProcessButtonEnabled).When(a => a.StopProcessButtonEnabled = false);
        }

        [Test]
        public void StopProcessButtonEnabled_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.StopProcessButtonEnabled = true;

            Assert.IsTrue(_step5ViewModelMock.Object.StopProcessButtonEnabled);
        }

        [Test]
        public void StopProcessButtonVisible_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.StopProcessButtonEnabled = false;

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.StopProcessButtonEnabled).When(a => a.StopProcessButtonEnabled = true);
        }

        [Test]
        public void StopProcessButtonVisible_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.StopProcessButtonVisible = false;

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.StopProcessButtonVisible).When(a => a.StopProcessButtonVisible = false);
        }

        [Test]
        public void StopProcessButtonVisible_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.StopProcessButtonVisible = true;

            Assert.IsTrue(_step5ViewModelMock.Object.StopProcessButtonVisible);
        }

        [Test]
        public void GoToPreviousStepButtonText_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.GoToPreviousStepButtonText = "111";

            Assert.AreEqual("111", _step5ViewModelMock.Object.GoToPreviousStepButtonText);
        }

        [Test]
        public void GoToPreviousStepButtonText_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.GoToPreviousStepButtonText = "xcsdsddddd";

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.GoToPreviousStepButtonText).When(a => a.GoToPreviousStepButtonText = "aaa");
        }

        [Test]
        public void GoToPreviousStepButtonText_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.GoToPreviousStepButtonText = "a";

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.GoToPreviousStepButtonText).When(a => a.GoToPreviousStepButtonText = "a");
        }

        [Test]
        public void GoToPreviousStepButtonVisible_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.GoToPreviousStepButtonVisible = false;

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.GoToPreviousStepButtonVisible).When(a => a.GoToPreviousStepButtonVisible = true);
        }

        [Test]
        public void GoToPreviousStepButtonVisible_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.GoToPreviousStepButtonVisible = false;

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.GoToPreviousStepButtonVisible).When(a => a.GoToPreviousStepButtonVisible = false);
        }

        [Test]
        public void GoToPreviousStepButtonVisible_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.GoToPreviousStepButtonVisible = true;

            Assert.IsTrue(_step5ViewModelMock.Object.GoToPreviousStepButtonVisible);
        }

        [Test]
        public void ProgressPercentage_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.ProgressPercentage = 44;

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.ProgressPercentage).When(a => a.ProgressPercentage = 55);
        }

        [Test]
        public void ProgressPercentage_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.ProgressPercentage = 44;

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.ProgressPercentage).When(a => a.ProgressPercentage = 44);
        }

        [Test]
        public void ProgressPercentage_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.ProgressPercentage = 44;

            Assert.AreEqual(44, _step5ViewModelMock.Object.ProgressPercentage);
        }

        [Test]
        public void ProgressInfoText_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.ProgressInfoText = "xcsdsddddd";

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.ProgressInfoText).When(a => a.ProgressInfoText = "aaa");
        }

        [Test]
        public void ProgressInfoText_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.ProgressInfoText = "a";

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.ProgressInfoText).When(a => a.ProgressInfoText = "a");
        }

        [Test]
        public void ProgressInfoText_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.ProgressInfoText = "111";

            Assert.AreEqual("111", _step5ViewModelMock.Object.ProgressInfoText);
        }

        [Test]
        public void FormTitle_SetValue_MustSetValueOnGlobalModelFormTitle()
        {
            string formTitleValue = "test_value";

            bool _valueIsSet = false;

            _globalWizardViewModelMock.SetupSet(x => x.FormTitle = It.IsAny<string>()).Callback<string>(value =>
            {
                if (value == formTitleValue)
                {
                    _valueIsSet = true;
                }
            });

            _step5ViewModelMock.Object.FormTitle = formTitleValue;

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void Settings_ReturnsGlobalViewModelRandomizerSettingsProperty()
        {
            _globalWizardViewModelMock.SetupGet(x => x.CopyWorkerSettings).Returns(new CopyWorkerSettings());

            Assert.AreEqual(_globalWizardViewModelMock.Object.CopyWorkerSettings, _step5ViewModelMock.Object.Settings);
        }

        [Test]
        public void ShowCopiedFilesButtonText_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.ShowCopiedFilesButtonText = "111";

            Assert.AreEqual("111", _step5ViewModelMock.Object.ShowCopiedFilesButtonText);
        }

        [Test]
        public void ShowCopiedFilesButtonText_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.ShowCopiedFilesButtonText = "xcsdsddddd";

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.ShowCopiedFilesButtonText).When(a => a.ShowCopiedFilesButtonText = "aaa");
        }

        [Test]
        public void ShowCopiedFilesButtonText_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.ShowCopiedFilesButtonText = "a";

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.ShowCopiedFilesButtonText).When(a => a.ShowCopiedFilesButtonText = "a");
        }

        [Test]
        public void ShowCopiedFilesButtonVisible_PropertyChanged_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.ShowCopiedFilesButtonVisible = false;

            _step5ViewModelMock.Object.ShouldNotifyOn(a => a.ShowCopiedFilesButtonVisible).When(a => a.ShowCopiedFilesButtonVisible = true);
        }

        [Test]
        public void ShowCopiedFilesButtonVisible_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step5ViewModelMock.Object.ShowCopiedFilesButtonVisible = false;

            _step5ViewModelMock.Object.ShouldNotNotifyOn(a => a.ShowCopiedFilesButtonVisible).When(a => a.ShowCopiedFilesButtonVisible = false);
        }

        [Test]
        public void ShowCopiedFilesButtonVisible_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step5ViewModelMock.Object.ShowCopiedFilesButtonVisible = true;

            Assert.IsTrue(_step5ViewModelMock.Object.ShowCopiedFilesButtonVisible);
        }
    }
}