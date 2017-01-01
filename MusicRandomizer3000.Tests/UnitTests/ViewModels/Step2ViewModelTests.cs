using MusicRandomizer3000.Core;
using MusicRandomizer3000.Core.Enums;
using MusicRandomizer3000.Core.Models;
using MusicRandomizer3000.Core.Services.Interfaces;
using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MusicRandomizer3000.Tests.UnitTests.ViewModels
{
    public class Step2ViewModelTests
    {
        private Mock<GlobalWizardViewModel> _globalWizardViewModelMock = null;
        private Mock<ISettingsService> _settingsServiceMock = null;
        private Mock<Step2ViewModel> _step2ViewModelMock = null;

        [SetUp]
        public void SetUp()
        {
            _globalWizardViewModelMock = new Mock<GlobalWizardViewModel>(It.IsAny<string>());
            _settingsServiceMock = new Mock<ISettingsService>();
            _step2ViewModelMock = new Mock<Step2ViewModel>(_globalWizardViewModelMock.Object, _settingsServiceMock.Object);
            _step2ViewModelMock.CallBase = true;
        }

        [Test]
        public void Constructor_SupplyInvalidGlobalWizardViewModel_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new Step2ViewModel(null, _settingsServiceMock.Object);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("globalModel"));
        }

        [Test]
        public void Constructor_SupplyInvalidSettingsService_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new Step2ViewModel(_globalWizardViewModelMock.Object, null);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("settingsService"));
        }

        [Test]
        public void Initialize_LoadSavedSettings_ViewModelIsInitializedWithLoadedSettings()
        {
            Step2Settings savedSettings = new Step2Settings
            {
                PathTo = "TestPathTo",
                OnCopyActionID = 1,
                SaveSettings = true
            };

            _settingsServiceMock.Setup(x => x.LoadSettings<Step2Settings>("Step2Settings")).Returns(savedSettings);

            _step2ViewModelMock.SetupGet(x => x.OnCopyOptions).Returns(new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>()
            {
                new ComboBoxOption { ID = 2 },
                new ComboBoxOption { ID = 1 },
                new ComboBoxOption { ID = 100 }
            }));

            Assert.AreEqual(savedSettings.PathTo, _step2ViewModelMock.Object.PathTo);
            Assert.AreEqual(_step2ViewModelMock.Object.OnCopyOptions[1], _step2ViewModelMock.Object.SelectedOnCopyOption);
            Assert.AreEqual(savedSettings.SaveSettings, _step2ViewModelMock.Object.SaveSettings);
        }

        [Test]
        public void Initialize_SavedSettingsDoNotExist_ViewModelIsInitializedWithDefaultValues()
        {
            _step2ViewModelMock.SetupGet(x => x.OnCopyOptions).Returns(new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>()
            {
                new ComboBoxOption { ID = 2 },
                new ComboBoxOption { ID = 1 },
                new ComboBoxOption { ID = 100 }
            }));

            Assert.IsNull(_step2ViewModelMock.Object.PathTo);
            Assert.AreEqual(_step2ViewModelMock.Object.OnCopyOptions.First(), _step2ViewModelMock.Object.SelectedOnCopyOption);
            Assert.AreEqual(false, _step2ViewModelMock.Object.SaveSettings);
        }

        [Test]
        public void PathTo_PropertyChanged_FiresPropertyChangedValue()
        {
            _step2ViewModelMock.Setup(a => a.Initialize());
            _step2ViewModelMock.Object.PathTo = "111111";

            _step2ViewModelMock.Object.ShouldNotifyOn(a => a.PathTo).When(a => a.PathTo = "vdfgfdgfdg");
        }

        [Test]
        public void PathTo_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step2ViewModelMock.Setup(a => a.Initialize());
            _step2ViewModelMock.Object.PathTo = "vdfgfdgfdg";

            _step2ViewModelMock.Object.ShouldNotNotifyOn(a => a.PathTo).When(a => a.PathTo = "vdfgfdgfdg");
        }

        [Test]
        public void SelectedOnCopyOption_PropertyChanged_FiresPropertyChangedValue()
        {
            _step2ViewModelMock.Setup(a => a.Initialize());
            _step2ViewModelMock.Object.SelectedOnCopyOption = null;

            _step2ViewModelMock.Object.ShouldNotifyOn(a => a.SelectedOnCopyOption).When(a => a.SelectedOnCopyOption = new ComboBoxOption());
        }

        [Test]
        public void SelectedOnCopyOption_PropertyChanged_FiresShowSelectedOnCopyOptionDescriptionPropertyChangedValue()
        {
            _step2ViewModelMock.Setup(a => a.Initialize());
            _step2ViewModelMock.Object.SelectedOnCopyOption = null;

            _step2ViewModelMock.Object.ShouldNotifyOn(a => a.ShowSelectedOnCopyOptionDescription).When(a => a.SelectedOnCopyOption = new ComboBoxOption());
        }

        [Test]
        public void SelectedOnCopyOption_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step2ViewModelMock.Setup(a => a.Initialize());
            _step2ViewModelMock.Object.SelectedOnCopyOption = null;

            _step2ViewModelMock.Object.ShouldNotNotifyOn(a => a.SelectedOnCopyOption).When(a => a.SelectedOnCopyOption = null);
        }

        [Test]
        public void SelectedOnCopyOption_PropertyDidNotChange_DoesNotFireShowSelectedOnCopyOptionDescriptionPropertyChangedValue()
        {
            _step2ViewModelMock.Setup(a => a.Initialize());
            _step2ViewModelMock.Object.SelectedOnCopyOption = null;

            _step2ViewModelMock.Object.ShouldNotNotifyOn(a => a.ShowSelectedOnCopyOptionDescription).When(a => a.SelectedOnCopyOption = null);
        }

        [Test]
        public void ShowSelectedOnCopyOptionDescription_SelectedOnCopyOptionIsNull_ReturnsFalse()
        {
            _step2ViewModelMock.SetupGet(a => a.SelectedOnCopyOption).Returns((ComboBoxOption)null);

            Assert.IsFalse(_step2ViewModelMock.Object.ShowSelectedOnCopyOptionDescription);
        }

        [Test]
        public void ShowSelectedOnCopyOptionDescription_SelectedOnCopyOptionDescriptionIsNull_ReturnsFalse()
        {
            _step2ViewModelMock.SetupGet(a => a.SelectedOnCopyOption).Returns(new ComboBoxOption { Description = "" });

            Assert.IsFalse(_step2ViewModelMock.Object.ShowSelectedOnCopyOptionDescription);
        }

        [Test]
        public void ShowSelectedOnCopyOptionDescription_SelectedOnCopyOptionDescriptionSupplied_ReturnsTrue()
        {
            _step2ViewModelMock.SetupGet(a => a.SelectedOnCopyOption).Returns(new ComboBoxOption { Description = "test" });

            Assert.IsTrue(_step2ViewModelMock.Object.ShowSelectedOnCopyOptionDescription);
        }

        [Test]
        public void SaveSettings_PropertyChanged_FiresPropertyChangedValue()
        {
            _step2ViewModelMock.Object.SaveSettings = false;

            _step2ViewModelMock.Object.ShouldNotifyOn(a => a.SaveSettings).When(a => a.SaveSettings = true);
        }

        [Test]
        public void SaveSettings_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step2ViewModelMock.Object.SaveSettings = false;

            _step2ViewModelMock.Object.ShouldNotNotifyOn(a => a.SaveSettings).When(a => a.SaveSettings = false);
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

            _step2ViewModelMock.Object.FormTitle = formTitleValue;

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_PathTo_MustSetValueOnGlobalModelRandomizerSettings()
        {
            bool _valueIsSet = false;

            _step2ViewModelMock.Object.PathTo = "value";
            _globalWizardViewModelMock.SetupGet(x => x.CopyWorkerSettings).Returns(new CopyWorkerSettings());
            _globalWizardViewModelMock
                .SetupSet(x => x.RandomizerWorkerSettings.PathTo = It.IsAny<string>())
                .Callback<string>(value =>
                {
                    if (value == _step2ViewModelMock.Object.PathTo)
                    {
                        _valueIsSet = true;
                    }
                });

            _step2ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_PathTo_MustSetValueOnGlobalModelCopierSettings()
        {
            bool _valueIsSet = false;

            _step2ViewModelMock.Object.PathTo = "value";

            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.PathTo = It.IsAny<string>())
                .Callback<string>(value =>
                {
                    if (value == _step2ViewModelMock.Object.PathTo)
                    {
                        _valueIsSet = true;
                    }
                });

            _step2ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_CharsPositionOnCopy_MustSetValueOnGlobalModelCopierSettings()
        {
            bool _valueIsSet = false;

            _step2ViewModelMock.Object.Initialize();
            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.CharsPositionOnCopy = It.IsAny<UniqueCharsPosition>())
                .Callback<UniqueCharsPosition>(value =>
                {
                    if (value == (UniqueCharsPosition)_step2ViewModelMock.Object.SelectedOnCopyOption.ID)
                    {
                        _valueIsSet = true;
                    }
                });

            _step2ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateSettings_SaveSettingsIsTrue_CallsSaveSettingsOnceAndDoesNotCallRemoveSettings()
        {
            _step2ViewModelMock.Object.SaveSettings = true;
            _settingsServiceMock.Setup(x => x.SaveSettings<Step2Settings>("Step2Settings", It.IsAny<Step2Settings>())).Verifiable();
            _settingsServiceMock.Setup(x => x.RemoveSettings("Step2Settings")).Verifiable();

            _step2ViewModelMock.Object.UpdateSettings();

            _settingsServiceMock.Verify(x => x.SaveSettings<Step2Settings>("Step2Settings", It.IsAny<Step2Settings>()), Times.Once);
            _settingsServiceMock.Verify(x => x.RemoveSettings("Step2Settings"), Times.Never);
        }

        [Test]
        public void UpdateSettings_SaveSettingsIsFalse_CallsRemoveSettingsOnceAndDoesNotCallSaveSettings()
        {
            _step2ViewModelMock.Object.SaveSettings = false;
            _settingsServiceMock.Setup(x => x.SaveSettings<Step2Settings>("Step2Settings", It.IsAny<Step2Settings>())).Verifiable();
            _settingsServiceMock.Setup(x => x.RemoveSettings("Step2Settings")).Verifiable();

            _step2ViewModelMock.Object.UpdateSettings();

            _settingsServiceMock.Verify(x => x.SaveSettings<Step2Settings>("Step2Settings", It.IsAny<Step2Settings>()), Times.Never);
            _settingsServiceMock.Verify(x => x.RemoveSettings("Step2Settings"), Times.Once);
        }
    }
}
