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
    [TestFixture]
    public class Step1ViewModelTests
    {
        private Mock<GlobalWizardViewModel> _globalWizardViewModelMock = null;
        private Mock<ISettingsService> _settingsServiceMock = null;
        private Mock<Step1ViewModel> _step1ViewModelMock = null;

        [SetUp]
        public void SetUp()
        {
            _globalWizardViewModelMock = new Mock<GlobalWizardViewModel>(It.IsAny<string>());
            _settingsServiceMock = new Mock<ISettingsService>();
            _step1ViewModelMock = new Mock<Step1ViewModel>(_globalWizardViewModelMock.Object, _settingsServiceMock.Object);
            _step1ViewModelMock.CallBase = true;
        }

        [Test]
        public void Constructor_SupplyInvalidGlobalWizardViewModel_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new Step1ViewModel(null, _settingsServiceMock.Object);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("globalModel"));
        }

        [Test]
        public void Constructor_SupplyInvalidSettingsService_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => new Step1ViewModel(_globalWizardViewModelMock.Object, null);

            Assert.That(testDelegate, Throws.InstanceOf<ArgumentNullException>().With.Property("ParamName").EqualTo("settingsService"));
        }

        [Test]
        public void Initialize_LoadSavedSettings_ViewModelIsInitializedWithLoadedSettings()
        {
            Step1Settings savedSettings = new Step1Settings
            {
                PathFrom = "TestPathFrom",
                SelectedFilesLimitID = 1,
                UseRecursiveSearch = true,
                SaveSettings = true,
                SelectedSizeID = 5,
                FindOnlyUniqueFiles = true,
                SizeLimit = 0.0001,
                FilesNumber = 1111,
                FoldersNumber = 22222,
                FilesInFolderNumber = 3333,
                OrganizeFiles = true
            };

            Mock<FilesSizeLimitSettings> filesSizeLimitSettingsMock = new Mock<FilesSizeLimitSettings>(It.IsAny<int>(), It.IsAny<double>());
            Mock<FilesNumberLimitSettings> filesNumberLimitSettingsMock = new Mock<FilesNumberLimitSettings>(It.IsAny<int>());
            Mock<FilesAndFoldersLimitSettings> filesAndFoldersLimitSettingsMock = new Mock<FilesAndFoldersLimitSettings>(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>());

            _settingsServiceMock.Setup(x => x.LoadSettings<Step1Settings>("Step1Settings")).Returns(savedSettings);

            _step1ViewModelMock.SetupGet(x => x.FilesLimitTypes).Returns(new ReadOnlyCollection<Limit>(new List<Limit>()
            {
                new Limit { ID = 2 },
                new Limit { ID = 1 },
                new Limit { ID = 100 }
            }));

            _step1ViewModelMock.Setup(x => x.GetFilesSizeLimitSettings(savedSettings)).Returns(filesSizeLimitSettingsMock.Object);
            _step1ViewModelMock.Setup(x => x.GetFilesNumberLimitSettings(savedSettings)).Returns(filesNumberLimitSettingsMock.Object);
            _step1ViewModelMock.Setup(x => x.GetFilesAndFoldersLimitSettings(savedSettings)).Returns(filesAndFoldersLimitSettingsMock.Object);

            Assert.AreEqual(savedSettings.PathFrom, _step1ViewModelMock.Object.PathFrom);
            Assert.AreEqual(_step1ViewModelMock.Object.FilesLimitTypes[1], _step1ViewModelMock.Object.SelectedFilesLimit);
            Assert.AreEqual(savedSettings.FindOnlyUniqueFiles, _step1ViewModelMock.Object.FindOnlyUniqueFiles);
            Assert.AreEqual(savedSettings.UseRecursiveSearch, _step1ViewModelMock.Object.UseRecursiveSearch);
            Assert.AreEqual(savedSettings.SaveSettings, _step1ViewModelMock.Object.SaveSettings);
            Assert.AreEqual(filesSizeLimitSettingsMock.Object, _step1ViewModelMock.Object.FilesSize);
            Assert.AreEqual(filesNumberLimitSettingsMock.Object, _step1ViewModelMock.Object.FilesNumber);
            Assert.AreEqual(filesAndFoldersLimitSettingsMock.Object, _step1ViewModelMock.Object.FilesAndFolders);
        }

        [Test]
        public void Initialize_SavedSettingsDoNotExist_ViewModelIsInitializedWithDefaultValues()
        {
            Mock<FilesSizeLimitSettings> filesSizeLimitSettingsMock = new Mock<FilesSizeLimitSettings>();
            Mock<FilesNumberLimitSettings> filesNumberLimitSettingsMock = new Mock<FilesNumberLimitSettings>();
            Mock<FilesAndFoldersLimitSettings> filesAndFoldersLimitSettingsMock = new Mock<FilesAndFoldersLimitSettings>();

            _step1ViewModelMock.SetupGet(x => x.FilesLimitTypes).Returns(new ReadOnlyCollection<Limit>(new List<Limit>()
            {
                new Limit { ID = 2 },
                new Limit { ID = 1 },
                new Limit { ID = 100 }
            }));

            _step1ViewModelMock.Setup(x => x.GetFilesSizeLimitSettings(null)).Returns(filesSizeLimitSettingsMock.Object);
            _step1ViewModelMock.Setup(x => x.GetFilesNumberLimitSettings(null)).Returns(filesNumberLimitSettingsMock.Object);
            _step1ViewModelMock.Setup(x => x.GetFilesAndFoldersLimitSettings(null)).Returns(filesAndFoldersLimitSettingsMock.Object);

            Assert.AreEqual("", _step1ViewModelMock.Object.PathFrom);
            Assert.AreEqual(_step1ViewModelMock.Object.FilesLimitTypes.First(), _step1ViewModelMock.Object.SelectedFilesLimit);
            Assert.AreEqual(true, _step1ViewModelMock.Object.FindOnlyUniqueFiles);
            Assert.AreEqual(true, _step1ViewModelMock.Object.UseRecursiveSearch);
            Assert.AreEqual(false, _step1ViewModelMock.Object.SaveSettings);
            Assert.AreEqual(filesSizeLimitSettingsMock.Object, _step1ViewModelMock.Object.FilesSize);
            Assert.AreEqual(filesNumberLimitSettingsMock.Object, _step1ViewModelMock.Object.FilesNumber);
            Assert.AreEqual(filesAndFoldersLimitSettingsMock.Object, _step1ViewModelMock.Object.FilesAndFolders);
        }

        [Test]
        public void PathFrom_PropertyChanged_FiresPropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.PathFrom = "111111";

            _step1ViewModelMock.Object.ShouldNotifyOn(a => a.PathFrom).When(a => a.PathFrom = "vdfgfdgfdg");
        }

        [Test]
        public void PathFrom_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.PathFrom = "vdfgfdgfdg";

            _step1ViewModelMock.Object.ShouldNotNotifyOn(a => a.PathFrom).When(a => a.PathFrom = "vdfgfdgfdg");
        }

        [Test]
        public void SelectedFilesLimit_PropertyChanged_FiresPropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.SelectedFilesLimit = null;

            _step1ViewModelMock.Object.ShouldNotifyOn(a => a.SelectedFilesLimit).When(a => a.SelectedFilesLimit = new Limit());
        }

        [Test]
        public void SelectedFilesLimit_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.SelectedFilesLimit = null;

            _step1ViewModelMock.Object.ShouldNotNotifyOn(a => a.SelectedFilesLimit).When(a => a.SelectedFilesLimit = null);
        }

        [Test]
        public void FindOnlyUniqueFiles_PropertyChanged_FiresPropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.FindOnlyUniqueFiles = false;

            _step1ViewModelMock.Object.ShouldNotifyOn(a => a.FindOnlyUniqueFiles).When(a => a.FindOnlyUniqueFiles = true);
        }

        [Test]
        public void FindOnlyUniqueFiles_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.FindOnlyUniqueFiles = false;

            _step1ViewModelMock.Object.ShouldNotNotifyOn(a => a.FindOnlyUniqueFiles).When(a => a.FindOnlyUniqueFiles = false);
        }

        [Test]
        public void UseRecursiveSearch_PropertyChanged_FiresPropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.UseRecursiveSearch = false;

            _step1ViewModelMock.Object.ShouldNotifyOn(a => a.UseRecursiveSearch).When(a => a.UseRecursiveSearch = true);
        }

        [Test]
        public void UseRecursiveSearch_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.UseRecursiveSearch = false;

            _step1ViewModelMock.Object.ShouldNotNotifyOn(a => a.UseRecursiveSearch).When(a => a.UseRecursiveSearch = false);
        }

        [Test]
        public void SaveSettings_PropertyChanged_FiresPropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.SaveSettings = false;

            _step1ViewModelMock.Object.ShouldNotifyOn(a => a.SaveSettings).When(a => a.SaveSettings = true);
        }

        [Test]
        public void SaveSettings_PropertyDidNotChange_DoesNotFirePropertyChangedValue()
        {
            _step1ViewModelMock.Setup(a => a.Initialize());
            _step1ViewModelMock.Object.SaveSettings = false;

            _step1ViewModelMock.Object.ShouldNotNotifyOn(a => a.SaveSettings).When(a => a.SaveSettings = false);
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

            _step1ViewModelMock.Object.FormTitle = formTitleValue;

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_FindOnlyUniqueFiles_MustSetValueOnGlobalModel()
        {
            bool _valueIsSet = false;

            _step1ViewModelMock.Object.FindOnlyUniqueFiles = true;
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.FindOnlyUniqueFiles).Callback((value) =>
            {
                if (value == _step1ViewModelMock.Object.FindOnlyUniqueFiles)
                {
                    _valueIsSet = true;
                }
            });

            _step1ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_UseRecursiveSearch_MustSetValueOnGlobalModel()
        {
            bool _valueIsSet = false;

            _step1ViewModelMock.Object.UseRecursiveSearch = true;
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.UseRecursiveSearch).Callback((value) =>
            {
                if (value == _step1ViewModelMock.Object.UseRecursiveSearch)
                {
                    _valueIsSet = true;
                }
            });

            _step1ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_PathFrom_MustSetValueOnGlobalModel()
        {
            bool _valueIsSet = false;

            _step1ViewModelMock.Object.PathFrom = "value";
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.PathFrom).Callback((value) =>
            {
                if (value == _step1ViewModelMock.Object.PathFrom)
                {
                    _valueIsSet = true;
                }
            });

            _step1ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_SizeLimitBytes_MustSetValueOnGlobalModelIfSelectedFilesLimitEqualsOne()
        {
            Mock<FilesSizeLimitSettings> filesSizeLimitSettingsMock = new Mock<FilesSizeLimitSettings>();
            filesSizeLimitSettingsMock.SetupGet(x => x.SizeLimitBytes).Returns(100);

            bool _valueIsSet = false;

            _step1ViewModelMock.Object.SelectedFilesLimit = _step1ViewModelMock.Object.FilesLimitTypes.Single(x => x.ID == 1);
            _step1ViewModelMock.SetupGet(x => x.FilesSize).Returns(filesSizeLimitSettingsMock.Object);
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.SizeLimitBytes).Callback((value) =>
            {
                if (value == filesSizeLimitSettingsMock.Object.SizeLimitBytes)
                {
                    _valueIsSet = true;
                }
            });

            _step1ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_SizeLimitBytes_MustNotSetValueOnGlobalModelIfSelectedFilesLimitDoesNotEqualOne()
        {
            _step1ViewModelMock.SetupGet(x => x.SelectedFilesLimit).Returns(new Limit());
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.SizeLimitBytes).Verifiable();

            _globalWizardViewModelMock.VerifySet(x => x.RandomizerWorkerSettings.SizeLimitBytes = It.IsAny<double>(), Times.Never);
        }

        [Test]
        public void UpdateGlobalModel_FilesNumberLimit_MustSetValueOnGlobalModelIfSelectedFilesLimitEqualsTwo()
        {
            Mock<FilesNumberLimitSettings> filesNumberLimitSettingsMock = new Mock<FilesNumberLimitSettings>();
            filesNumberLimitSettingsMock.SetupGet(x => x.Number).Returns(100);

            bool _valueIsSet = false;

            _step1ViewModelMock.Object.SelectedFilesLimit = _step1ViewModelMock.Object.FilesLimitTypes.Single(x => x.ID == 2);
            _step1ViewModelMock.SetupGet(x => x.FilesNumber).Returns(filesNumberLimitSettingsMock.Object);
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.FilesNumberLimit).Callback((value) =>
            {
                if (value == filesNumberLimitSettingsMock.Object.Number)
                {
                    _valueIsSet = true;
                }
            });

            _step1ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_FilesNumberLimit_MustNotSetValueOnGlobalModelIfSelectedFilesLimitDoesNotEqualTwo()
        {
            _step1ViewModelMock.SetupGet(x => x.SelectedFilesLimit).Returns(new Limit());
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.FilesNumberLimit).Verifiable();

            _globalWizardViewModelMock.VerifySet(x => x.RandomizerWorkerSettings.FilesNumberLimit = It.IsAny<int>(), Times.Never);
        }

        [Test]
        public void UpdateGlobalModel_FilesInFolderNumberLimit_MustSetValueOnGlobalModelIfSelectedFilesLimitEqualsThree()
        {
            Mock<FilesAndFoldersLimitSettings> filesAndFoldersLimitSettingsMock = new Mock<FilesAndFoldersLimitSettings>();
            filesAndFoldersLimitSettingsMock.SetupGet(x => x.FilesNumber).Returns(100);

            bool _valueIsSet = false;

            _step1ViewModelMock.Object.SelectedFilesLimit = _step1ViewModelMock.Object.FilesLimitTypes.Single(x => x.ID == 3);
            _step1ViewModelMock.SetupGet(x => x.FilesAndFolders).Returns(filesAndFoldersLimitSettingsMock.Object);
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.FilesNumberPerFolderLimit).Callback((value) =>
            {
                if (value == filesAndFoldersLimitSettingsMock.Object.FilesNumber)
                {
                    _valueIsSet = true;
                }
            });

            _step1ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_FilesInFolderNumberLimit_MustNotSetValueOnGlobalModelIfSelectedFilesLimitDoesNotEqualThree()
        {
            _step1ViewModelMock.SetupGet(x => x.SelectedFilesLimit).Returns(new Limit());
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.FilesNumberPerFolderLimit).Verifiable();

            _globalWizardViewModelMock.VerifySet(x => x.RandomizerWorkerSettings.FilesNumberPerFolderLimit = It.IsAny<int>(), Times.Never);
        }

        [Test]
        public void UpdateGlobalModel_FoldersNumberLimit_MustSetValueOnGlobalModelIfSelectedFilesLimitEqualsThree()
        {
            Mock<FilesAndFoldersLimitSettings> filesAndFoldersLimitSettingsMock = new Mock<FilesAndFoldersLimitSettings>();
            filesAndFoldersLimitSettingsMock.SetupGet(x => x.FoldersNumber).Returns(100);

            bool _valueIsSet = false;

            _step1ViewModelMock.Object.SelectedFilesLimit = _step1ViewModelMock.Object.FilesLimitTypes.Single(x => x.ID == 3);
            _step1ViewModelMock.SetupGet(x => x.FilesAndFolders).Returns(filesAndFoldersLimitSettingsMock.Object);
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.FoldersNumberLimit).Callback((value) =>
            {
                if (value == filesAndFoldersLimitSettingsMock.Object.FoldersNumber)
                {
                    _valueIsSet = true;
                }
            });

            _step1ViewModelMock.Object.UpdateGlobalModel();

            Assert.IsTrue(_valueIsSet);
        }

        [Test]
        public void UpdateGlobalModel_FoldersNumberLimit_MustNotSetValueOnGlobalModelIfSelectedFilesLimitDoesNotEqualThree()
        {
            _step1ViewModelMock.SetupGet(x => x.SelectedFilesLimit).Returns(new Limit());
            _globalWizardViewModelMock.SetupSet(x => x.RandomizerWorkerSettings.FoldersNumberLimit).Verifiable();

            _globalWizardViewModelMock.VerifySet(x => x.RandomizerWorkerSettings.FoldersNumberLimit = It.IsAny<int>(), Times.Never);
        }

        [Test]
        public void UpdateSettings_SaveSettingsIsTrue_CallsSaveSettingsOnceAndDoesNotCallRemoveSettings()
        {
            _step1ViewModelMock.Object.SaveSettings = true;
            _settingsServiceMock.Setup(x => x.SaveSettings<Step1Settings>("Step1Settings", It.IsAny<Step1Settings>())).Verifiable();
            _settingsServiceMock.Setup(x => x.RemoveSettings("Step1Settings")).Verifiable();

            _step1ViewModelMock.Object.UpdateSettings();

            _settingsServiceMock.Verify(x => x.SaveSettings<Step1Settings>("Step1Settings", It.IsAny<Step1Settings>()), Times.Once);
            _settingsServiceMock.Verify(x => x.RemoveSettings("Step1Settings"), Times.Never);
        }

        [Test]
        public void UpdateSettings_SaveSettingsIsFalse_CallsRemoveSettingsOnceAndDoesNotCallSaveSettings()
        {
            _step1ViewModelMock.Object.SaveSettings = false;
            _settingsServiceMock.Setup(x => x.SaveSettings<Step1Settings>("Step1Settings", It.IsAny<Step1Settings>())).Verifiable();
            _settingsServiceMock.Setup(x => x.RemoveSettings("Step1Settings")).Verifiable();

            _step1ViewModelMock.Object.UpdateSettings();

            _settingsServiceMock.Verify(x => x.SaveSettings<Step1Settings>("Step1Settings", It.IsAny<Step1Settings>()), Times.Never);
            _settingsServiceMock.Verify(x => x.RemoveSettings("Step1Settings"), Times.Once);
        }
    }
}