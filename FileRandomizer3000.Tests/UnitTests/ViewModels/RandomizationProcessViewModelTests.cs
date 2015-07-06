using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;

namespace FileRandomizer3000.Tests.UnitTests.ViewModels
{
    public class RandomizationProcessViewModelTests
    {
        private Mock<GlobalWizardViewModel> _globalWizardViewModelMock = null;
        private Mock<RandomizationProcessViewModel> _step4ViewModelMock = null;

        [SetUp]
        public void SetUp()
        {
            _globalWizardViewModelMock = new Mock<GlobalWizardViewModel>(It.IsAny<string>());
            _step4ViewModelMock = new Mock<RandomizationProcessViewModel>(_globalWizardViewModelMock.Object);
            _step4ViewModelMock.CallBase = true;
        }

        [Test]
        public void Constructor_SupplyInvalidGlobalWizardViewModel_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new RandomizationProcessViewModel(null);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("globalModel"));
        }

        [Test]
        public void StopProcessButtonText_PropertyChanged_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.StopProcessButtonText = "svcxvcvbcbfgg";

            _step4ViewModelMock.Object.ShouldNotifyOn(a => a.StopProcessButtonText).When(a => a.StopProcessButtonText = "xcvxcvxcv");
        }

        [Test]
        public void StopProcessButtonText_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.StopProcessButtonText = null;

            _step4ViewModelMock.Object.ShouldNotNotifyOn(a => a.StopProcessButtonText).When(a => a.StopProcessButtonText = null);
        }

        [Test]
        public void StopProcessButtonText_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step4ViewModelMock.Object.StopProcessButtonText = "1";

            Assert.AreEqual("1", _step4ViewModelMock.Object.StopProcessButtonText);
        }

        [Test]
        public void StopProcessButtonEnabled_PropertyChanged_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.StopProcessButtonEnabled = false;

            _step4ViewModelMock.Object.ShouldNotifyOn(a => a.StopProcessButtonEnabled).When(a => a.StopProcessButtonEnabled = true);
        }

        [Test]
        public void StopProcessButtonEnabled_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.StopProcessButtonEnabled = false;

            _step4ViewModelMock.Object.ShouldNotNotifyOn(a => a.StopProcessButtonEnabled).When(a => a.StopProcessButtonEnabled = false);
        }

        [Test]
        public void StopProcessButtonEnabled_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step4ViewModelMock.Object.StopProcessButtonEnabled = true;

            Assert.IsTrue(_step4ViewModelMock.Object.StopProcessButtonEnabled);
        }

        [Test]
        public void StopProcessButtonVisible_PropertyChanged_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.StopProcessButtonEnabled = false;

            _step4ViewModelMock.Object.ShouldNotifyOn(a => a.StopProcessButtonEnabled).When(a => a.StopProcessButtonEnabled = true);
        }

        [Test]
        public void StopProcessButtonVisible_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.StopProcessButtonVisible = false;

            _step4ViewModelMock.Object.ShouldNotNotifyOn(a => a.StopProcessButtonVisible).When(a => a.StopProcessButtonVisible = false);
        }

        [Test]
        public void StopProcessButtonVisible_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step4ViewModelMock.Object.StopProcessButtonVisible = true;

            Assert.IsTrue(_step4ViewModelMock.Object.StopProcessButtonVisible);
        }

        [Test]
        public void GoToPreviousStepButtonText_PropertyChanged_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.GoToPreviousStepButtonText = "xcsdsddddd";

            _step4ViewModelMock.Object.ShouldNotifyOn(a => a.GoToPreviousStepButtonText).When(a => a.GoToPreviousStepButtonText = "aaa");
        }

        [Test]
        public void GoToPreviousStepButtonText_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.GoToPreviousStepButtonText = "a";

            _step4ViewModelMock.Object.ShouldNotNotifyOn(a => a.GoToPreviousStepButtonText).When(a => a.GoToPreviousStepButtonText = "a");
        }

        [Test]
        public void GoToPreviousStepButtonText_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step4ViewModelMock.Object.GoToPreviousStepButtonText = "111";

            Assert.AreEqual("111", _step4ViewModelMock.Object.GoToPreviousStepButtonText);
        }

        [Test]
        public void GoToPreviousStepButtonVisible_PropertyChanged_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.GoToPreviousStepButtonVisible = false;

            _step4ViewModelMock.Object.ShouldNotifyOn(a => a.GoToPreviousStepButtonVisible).When(a => a.GoToPreviousStepButtonVisible = true);
        }

        [Test]
        public void GoToPreviousStepButtonVisible_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.GoToPreviousStepButtonVisible = false;

            _step4ViewModelMock.Object.ShouldNotNotifyOn(a => a.GoToPreviousStepButtonVisible).When(a => a.GoToPreviousStepButtonVisible = false);
        }

        [Test]
        public void GoToPreviousStepButtonVisible_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step4ViewModelMock.Object.GoToPreviousStepButtonVisible = true;

            Assert.IsTrue(_step4ViewModelMock.Object.GoToPreviousStepButtonVisible);
        }

        [Test]
        public void InfoText_PropertyChanged_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.InfoText = "xcsdsddddd";

            _step4ViewModelMock.Object.ShouldNotifyOn(a => a.InfoText).When(a => a.InfoText = "aaa");
        }

        [Test]
        public void InfoText_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.InfoText = "a";

            _step4ViewModelMock.Object.ShouldNotNotifyOn(a => a.InfoText).When(a => a.InfoText = "a");
        }

        [Test]
        public void InfoText_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step4ViewModelMock.Object.InfoText = "111";

            Assert.AreEqual("111", _step4ViewModelMock.Object.InfoText);
        }

        [Test]
        public void InfoVisible_PropertyChanged_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.InfoVisible = false;

            _step4ViewModelMock.Object.ShouldNotifyOn(a => a.InfoVisible).When(a => a.InfoVisible = true);
        }

        [Test]
        public void InfoVisible_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.InfoVisible = false;

            _step4ViewModelMock.Object.ShouldNotNotifyOn(a => a.InfoVisible).When(a => a.InfoVisible = false);
        }

        [Test]
        public void InfoVisible_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step4ViewModelMock.Object.InfoVisible = true;

            Assert.IsTrue(_step4ViewModelMock.Object.InfoVisible);
        }

        [Test]
        public void SpinnerImageVisible_PropertyChanged_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.SpinnerImageVisible = false;

            _step4ViewModelMock.Object.ShouldNotifyOn(a => a.SpinnerImageVisible).When(a => a.SpinnerImageVisible = true);
        }

        [Test]
        public void SpinnerImageVisible_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step4ViewModelMock.Object.SpinnerImageVisible = false;

            _step4ViewModelMock.Object.ShouldNotNotifyOn(a => a.SpinnerImageVisible).When(a => a.SpinnerImageVisible = false);
        }

        [Test]
        public void SpinnerImageVisible_ReturnsSetValue()
        {
            // this is stupid, but Moq won't allow to set up a mock setter on a property with complex logic
            // https://github.com/Moq/moq4/issues/124
            _step4ViewModelMock.Object.SpinnerImageVisible = true;

            Assert.IsTrue(_step4ViewModelMock.Object.SpinnerImageVisible);
        }

        [Test]
        public void FormTitle_SetValue_MustSetValueOnGlobalModelFormTitle()
        {
            string formTitleValue = "test_value";

            bool _valueIsSet = false;

            _globalWizardViewModelMock.SetupSet(x => x.FormTitle).Callback((value) =>
            {
                if (value == formTitleValue)
                {
                    _valueIsSet = true;
                }
            });

            _step4ViewModelMock.Object.FormTitle = formTitleValue;

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void Settings_ReturnsGlobalViewModelRandomizerSettingsProperty()
        {
            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());

            Assert.AreEqual(_globalWizardViewModelMock.Object.RandomizerWorkerSettings, _step4ViewModelMock.Object.Settings);
        }
    }
}
