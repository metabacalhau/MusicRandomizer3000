using FileRandomizer3000.Core;
using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FileRandomizer3000.Tests.UnitTests.ViewModels
{
    public class Step3ViewModelTests
    {
        private Mock<GlobalWizardViewModel> _globalWizardViewModelMock = null;
        private Mock<ISettingsService> _settingsServiceMock = null;
        private Mock<Step3ViewModel> _step3ViewModelMock = null;

        [SetUp]
        public void SetUp()
        {
            _globalWizardViewModelMock = new Mock<GlobalWizardViewModel>(It.IsAny<string>());
            _settingsServiceMock = new Mock<ISettingsService>();
            _step3ViewModelMock = new Mock<Step3ViewModel>(_globalWizardViewModelMock.Object, _settingsServiceMock.Object);
            _step3ViewModelMock.CallBase = true;
        }

        [Test]
        public void Constructor_SupplyInvalidGlobalWizardViewModel_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new Step3ViewModel(null, _settingsServiceMock.Object);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("globalModel"));
        }

        [Test]
        public void Constructor_SupplyInvalidSettingsService_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new Step3ViewModel(_globalWizardViewModelMock.Object, null);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("settingsService"));
        }

        [Test]
        public void Initialize_LoadSavedSettings_ViewModelIsInitializedWithLoadedSettings()
        {
            Step3Settings savedSettings = new Step3Settings
            {
                OnDuplicateOptionID = 1,
                SortOptionID = 1,
                ShowRandomizedFiles = true,
                SaveSettings = true
            };

            _settingsServiceMock.Setup(x => x.LoadSettings<Step3Settings>("Step3Settings")).Returns(savedSettings);

            _step3ViewModelMock.SetupGet(x => x.OnDuplicateOptions).Returns(new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>()
            {
                new ComboBoxOption { ID = 2 },
                new ComboBoxOption { ID = 1 },
                new ComboBoxOption { ID = 100 }
            }));

            _step3ViewModelMock.SetupGet(x => x.SortOptions).Returns(new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>()
            {
                new ComboBoxOption { ID = 2 },
                new ComboBoxOption { ID = 1 },
                new ComboBoxOption { ID = 100 }
            }));

            Assert.AreEqual(_step3ViewModelMock.Object.SortOptions[1], _step3ViewModelMock.Object.SelectedSortOption);
            Assert.AreEqual(_step3ViewModelMock.Object.OnDuplicateOptions[1], _step3ViewModelMock.Object.SelectedOnDuplicateOption);
            Assert.AreEqual(savedSettings.ShowRandomizedFiles, _step3ViewModelMock.Object.ShowRandomizedFiles);
            Assert.AreEqual(savedSettings.SaveSettings, _step3ViewModelMock.Object.SaveSettings);
        }

        [Test]
        public void Initialize_SavedSettingsDoNotExist_ViewModelIsInitializedWithDefaultValues()
        {
            _step3ViewModelMock.SetupGet(x => x.OnDuplicateOptions).Returns(new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>()
            {
                new ComboBoxOption { ID = 2 },
                new ComboBoxOption { ID = 1 },
                new ComboBoxOption { ID = 100 }
            }));

            _step3ViewModelMock.SetupGet(x => x.SortOptions).Returns(new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>()
            {
                new ComboBoxOption { ID = 2 },
                new ComboBoxOption { ID = 1 },
                new ComboBoxOption { ID = 100 }
            }));

            Assert.AreEqual(_step3ViewModelMock.Object.SortOptions.First(), _step3ViewModelMock.Object.SelectedSortOption);
            Assert.AreEqual(_step3ViewModelMock.Object.OnDuplicateOptions.First(), _step3ViewModelMock.Object.SelectedOnDuplicateOption);
            Assert.IsFalse(_step3ViewModelMock.Object.ShowRandomizedFiles);
            Assert.IsFalse(_step3ViewModelMock.Object.SaveSettings);
        }

        [Test]
        public void SelectedOnDuplicateOption_PropertyChanged_FiresPropertyChangedValue()
        {
            _step3ViewModelMock.Object.SelectedOnDuplicateOption = new ComboBoxOption();

            _step3ViewModelMock.Object.ShouldNotifyOn(a => a.SelectedOnDuplicateOption).When(a => a.SelectedOnDuplicateOption = new ComboBoxOption());
        }

        [Test]
        public void SelectedOnDuplicateOption_PropertyChanged_FiresShowSelectedOnDuplicateOptionDescriptionPropertyChangedValue()
        {
            _step3ViewModelMock.Object.SelectedOnDuplicateOption = new ComboBoxOption();

            _step3ViewModelMock.Object.ShouldNotifyOn(a => a.ShowSelectedOnDuplicateOptionDescription).When(a => a.SelectedOnDuplicateOption = new ComboBoxOption());
        }

        [Test]
        public void SelectedOnDuplicateOption_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step3ViewModelMock.Object.SelectedOnDuplicateOption = null;

            _step3ViewModelMock.Object.ShouldNotNotifyOn(a => a.SelectedOnDuplicateOption).When(a => a.SelectedOnDuplicateOption = null);
        }

        [Test]
        public void SelectedOnDuplicateOption_PropertyDidNotChange_FiresShowSelectedOnDuplicateOptionDescriptionPropertyChangedValue()
        {
            _step3ViewModelMock.Object.SelectedOnDuplicateOption = null;

            _step3ViewModelMock.Object.ShouldNotNotifyOn(a => a.ShowSelectedOnDuplicateOptionDescription).When(a => a.SelectedOnDuplicateOption = null);
        }

        [Test]
        public void ShowSelectedOnDuplicateOptionDescription_SelectedOnCopyOptionIsNull_ReturnsFalse()
        {
            _step3ViewModelMock.SetupGet(a => a.SelectedOnDuplicateOption).Returns((ComboBoxOption)null);

            Assert.IsFalse(_step3ViewModelMock.Object.ShowSelectedOnDuplicateOptionDescription);
        }

        [Test]
        public void ShowSelectedOnCopyOptionDescription_SelectedOnDuplicateOptionDescriptionIsNull_ReturnsFalse()
        {
            _step3ViewModelMock.SetupGet(a => a.SelectedOnDuplicateOption).Returns(new ComboBoxOption { Description = "" });

            Assert.IsFalse(_step3ViewModelMock.Object.ShowSelectedOnDuplicateOptionDescription);
        }

        [Test]
        public void ShowSelectedOnCopyOptionDescription_SelectedOnDuplicateOptionDescriptionSupplied_ReturnsTrue()
        {
            _step3ViewModelMock.SetupGet(a => a.SelectedOnDuplicateOption).Returns(new ComboBoxOption { Description = "test" });

            Assert.IsTrue(_step3ViewModelMock.Object.ShowSelectedOnDuplicateOptionDescription);
        }

        [Test]
        public void SelectedSortOption_PropertyChanged_FiresPropertyChangedValue()
        {
            _step3ViewModelMock.Object.SelectedSortOption = new ComboBoxOption();

            _step3ViewModelMock.Object.ShouldNotifyOn(a => a.SelectedSortOption).When(a => a.SelectedSortOption = new ComboBoxOption());
        }

        [Test]
        public void SelectedSortOption_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step3ViewModelMock.Object.SelectedSortOption = null;

            _step3ViewModelMock.Object.ShouldNotNotifyOn(a => a.SelectedSortOption).When(a => a.SelectedSortOption = null);
        }

        [Test]
        public void ShowRandomizedFiles_PropertyChanged_FiresPropertyChangedValue()
        {
            _step3ViewModelMock.Object.ShowRandomizedFiles = true;

            _step3ViewModelMock.Object.ShouldNotifyOn(a => a.ShowRandomizedFiles).When(a => a.ShowRandomizedFiles = false);
        }

        [Test]
        public void ShowRandomizedFiles_PropertyChanged_FiresNextButtonTextPropertyChangedValue()
        {
            _step3ViewModelMock.Object.ShowRandomizedFiles = true;

            _step3ViewModelMock.Object.ShouldNotifyOn(a => a.NextButtonText).When(a => a.ShowRandomizedFiles = false);
        }

        [Test]
        public void ShowRandomizedFiles_PropertyDidNotChange_FiresPropertyChangedValue()
        {
            _step3ViewModelMock.Object.ShowRandomizedFiles = false;

            _step3ViewModelMock.Object.ShouldNotNotifyOn(a => a.ShowRandomizedFiles).When(a => a.ShowRandomizedFiles = false);
        }

        [Test]
        public void ShowRandomizedFiles_PropertyDidNotChange_FiresNextButtonTextPropertyChangedValue()
        {
            _step3ViewModelMock.Object.ShowRandomizedFiles = true;

            _step3ViewModelMock.Object.ShouldNotNotifyOn(a => a.NextButtonText).When(a => a.ShowRandomizedFiles = true);
        }

        [Test]
        public void NextButtonText_ShowRandomizedFilesIsTrue_ReturnsExpectedResult()
        {
            _step3ViewModelMock.Setup(x => x.ShowRandomizedFiles).Returns(true);

            Assert.AreEqual("Randomize and Show", _step3ViewModelMock.Object.NextButtonText);
        }

        [Test]
        public void NextButtonText_ShowRandomizedFilesIsFalse_ReturnsExpectedResult()
        {
            _step3ViewModelMock.Setup(x => x.ShowRandomizedFiles).Returns(false);

            Assert.AreEqual("Randomize and Copy", _step3ViewModelMock.Object.NextButtonText);
        }

        [Test]
        public void SaveSettings_PropertyChanged_FiresPropertyChangedValue()
        {
            _step3ViewModelMock.Object.SaveSettings = false;

            _step3ViewModelMock.Object.ShouldNotifyOn(a => a.SaveSettings).When(a => a.SaveSettings = true);
        }

        [Test]
        public void SaveSettings_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step3ViewModelMock.Object.SaveSettings = false;

            _step3ViewModelMock.Object.ShouldNotNotifyOn(a => a.SaveSettings).When(a => a.SaveSettings = false);
        }

        [Test]
        public void FormTitle_SetValue_MustSetValueOnGlobalModelFormTitle()
        {
            string formTitleValue = "test_value";

            bool _valueIsSet = false;

            _globalWizardViewModelMock.SetupSet(x => x.FormTitle = It.IsAny<string>())
                .Callback<string>(value =>
                {
                    if (value == formTitleValue)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.FormTitle = formTitleValue;

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_OnDuplicateDoNotCopy_MustSetValueOnGlobalModelRandomizerSettings()
        {
            bool _valueIsSet = false;

            _step3ViewModelMock.Object.SelectedOnDuplicateOption = new ComboBoxOption { ID = 1 };
            _globalWizardViewModelMock.SetupGet(x => x.CopyWorkerSettings).Returns(new CopyWorkerSettings());
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.OnDuplicateDoNotCopy = It.IsAny<bool>())
                .Callback<bool>(value =>
                {
                    if (value == true)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_OnDuplicateDoNotCopy_MustSetValueOnGlobalModelCopierSettings()
        {
            bool _valueIsSet = false;

            _step3ViewModelMock.Object.SelectedOnDuplicateOption = new ComboBoxOption { ID = 1 };
            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.OnDuplicateDoNotCopy = It.IsAny<bool>())
                .Callback<bool>(value =>
                {
                    if (value == true)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_OnDuplicateOverwrite_MustSetValueOnGlobalModelCopierSettings()
        {
            bool _valueIsSet = false;

            _step3ViewModelMock.Object.SelectedOnDuplicateOption = new ComboBoxOption { ID = 2 };
            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.OnDuplicateOverwrite = It.IsAny<bool>())
                .Callback<bool>(value =>
                {
                    if (value == true)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_OnDuplicateAddPrefix_MustSetValueOnGlobalModelCopierSettings()
        {
            bool _valueIsSet = false;

            _step3ViewModelMock.Object.SelectedOnDuplicateOption = new ComboBoxOption { ID = 3 };
            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.OnDuplicateAddPrefix = It.IsAny<bool>())
                .Callback<bool>(value =>
                {
                    if (value == true)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_OnDuplicateAddSuffix_MustSetValueOnGlobalModelCopierSettings()
        {
            bool _valueIsSet = false;

            _step3ViewModelMock.Object.SelectedOnDuplicateOption = new ComboBoxOption { ID = 4 };
            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.OnDuplicateAddSuffix = It.IsAny<bool>())
                .Callback<bool>(value =>
                {
                    if (value == true)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_CharsPositionOnDuplicate_SetsExpectedValueOnGlobalModelCopierSettingsWhenOnDuplicateAddSuffixPropertyIsTrue()
        {
            bool _valueIsSet = false;

            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupGet(x => x.CopyWorkerSettings.OnDuplicateAddSuffix).Returns(true);
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.CharsPositionOnDuplicate = It.IsAny<UniqueCharsPosition>())
                .Callback<UniqueCharsPosition>(value =>
                {
                    if (value == UniqueCharsPosition.Suffix)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_CharsPositionOnDuplicate_SetsExpectedValueOnGlobalModelCopierSettingsWhenOnDuplicateAddPrefixPropertyIsTrue()
        {
            bool _valueIsSet = false;

            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupGet(x => x.CopyWorkerSettings.OnDuplicateAddPrefix).Returns(true);
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.CharsPositionOnDuplicate = It.IsAny<UniqueCharsPosition>())
                .Callback<UniqueCharsPosition>(value =>
                {
                    if (value == UniqueCharsPosition.Prefix)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_CharsPositionOnDuplicate_SetsValueToNoneOnGlobalModelCopierSettings()
        {
            bool _valueIsSet = false;

            _step3ViewModelMock.Object.SelectedOnDuplicateOption = new ComboBoxOption { ID = 100 };
            _globalWizardViewModelMock.SetupGet(x => x.RandomizerWorkerSettings).Returns(new RandomizerWorkerSettings());
            _globalWizardViewModelMock.SetupSet(x => x.CopyWorkerSettings.CharsPositionOnDuplicate = It.IsAny<UniqueCharsPosition>())
                .Callback<UniqueCharsPosition>(value =>
                {
                    if (value == UniqueCharsPosition.None)
                    {
                        _valueIsSet = true;
                    }
                });

            _step3ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateSettings_SaveSettingsIsTrue_CallsSaveSettingsOnceAndDoesNotCallRemoveSettings()
        {
            _step3ViewModelMock.Object.SaveSettings = true;
            _settingsServiceMock.Setup(x => x.SaveSettings<Step3Settings>("Step3Settings", It.IsAny<Step3Settings>())).Verifiable();
            _settingsServiceMock.Setup(x => x.RemoveSettings("Step3Settings")).Verifiable();

            _step3ViewModelMock.Object.UpdateSettings();

            _settingsServiceMock.Verify(x => x.SaveSettings<Step3Settings>("Step3Settings", It.IsAny<Step3Settings>()), Times.Once);
            _settingsServiceMock.Verify(x => x.RemoveSettings("Step3Settings"), Times.Never);
        }

        [Test]
        public void UpdateSettings_SaveSettingsIsFalse_CallsRemoveSettingsOnceAndDoesNotCallSaveSettings()
        {
            _step3ViewModelMock.Object.SaveSettings = false;
            _settingsServiceMock.Setup(x => x.SaveSettings<Step3Settings>("Step3Settings", It.IsAny<Step3Settings>())).Verifiable();
            _settingsServiceMock.Setup(x => x.RemoveSettings("Step3Settings")).Verifiable();

            _step3ViewModelMock.Object.UpdateSettings();

            _settingsServiceMock.Verify(x => x.SaveSettings<Step3Settings>("Step3Settings", It.IsAny<Step3Settings>()), Times.Never);
            _settingsServiceMock.Verify(x => x.RemoveSettings("Step3Settings"), Times.Once);
        }
    }
}
