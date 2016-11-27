using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FileRandomizer3000.Core.ViewModels
{
    public class Step1ViewModel : ViewModelBase
    {
        private readonly GlobalWizardViewModel _globalModel;
        private readonly ISettingsService _settingsService;

        public Step1ViewModel(GlobalWizardViewModel globalModel, ISettingsService settingsService)
        {
            if (globalModel == null) throw new ArgumentNullException("globalModel");
            if (settingsService == null) throw new ArgumentNullException("settingsService");

            _globalModel = globalModel;
            _settingsService = settingsService;

            Initialize();
        }

        private ReadOnlyCollection<Limit> _filesLimitTypes = null;
        public virtual ReadOnlyCollection<Limit> FilesLimitTypes
        {
            get
            {
                if (_filesLimitTypes == null)
                {
                    _filesLimitTypes = new ReadOnlyCollection<Limit>(new List<Limit>
                    {
                        new Limit { ID = (int)LimitType.FilesTotalSize, Title = "total size" },
                        new Limit { ID = (int)LimitType.FilesNumber, Title = "number", MinValue = 1, MaxValue = 10000 },
                        new Limit { ID = (int)LimitType.FilesNumberPerFolder, Title = "folders" }
                    });
                }

                return _filesLimitTypes;
            }
        }

        private string _pathsFromLabel;
        public virtual string PathsFromLabel
        {
            get
            {
                return _pathsFromLabel;
            }
            set
            {
                if (CheckPropertyChanged<string>("PathsFromLabel", ref _pathsFromLabel, ref value))
                {
                    FirePropertyChanged("PathsFromLabel");
                }
            }
        }

        private bool _pathsFromLabelVisible;
        public virtual bool PathsFromLabelVisible
        {
            get
            {
                return _pathsFromLabelVisible;
            }
            set
            {
                if (CheckPropertyChanged<bool>("PathsFromLabelVisible", ref _pathsFromLabelVisible, ref value))
                {
                    FirePropertyChanged("PathsFromLabelVisible");
                }
            }
        }

        private List<string> _pathsFrom;
        public virtual List<string> PathsFrom
        {
            get
            {
                return _pathsFrom;
            }
            set
            {
                if (CheckPropertyChanged<List<string>>("PathsFrom", ref _pathsFrom, ref value))
                {
                    FirePropertyChanged("PathsFrom");
                }
            }
        }

        private Limit _selectedFilesLimit;
        public virtual Limit SelectedFilesLimit
        {
            get
            {
                return _selectedFilesLimit;
            }
            set
            {
                if (CheckPropertyChanged<Limit>("SelectedFilesLimit", ref _selectedFilesLimit, ref value))
                {
                    FirePropertyChanged("SelectedFilesLimit");
                }
            }
        }

        private bool _findOnlyUniqueFiles;
        public virtual bool FindOnlyUniqueFiles
        {
            get
            {
                return _findOnlyUniqueFiles;
            }
            set
            {
                if (CheckPropertyChanged<bool>("FindOnlyUniqueFiles", ref _findOnlyUniqueFiles, ref value))
                {
                    FirePropertyChanged("FindOnlyUniqueFiles");
                }
            }
        }

        private bool _saveSettings;
        public virtual bool SaveSettings
        {
            get
            {
                return _saveSettings;
            }
            set
            {
                if (CheckPropertyChanged<bool>("SaveSettings", ref _saveSettings, ref value))
                {
                    FirePropertyChanged("SaveSettings");
                }
            }
        }

        public virtual string FormTitle
        {
            set
            {
                _globalModel.FormTitle = value;
            }
        }

        public virtual FilesAndFoldersLimitSettings FilesAndFolders { get; private set; }

        public virtual FilesNumberLimitSettings FilesNumber { get; private set; }

        public virtual FilesSizeLimitSettings FilesSize { get; private set; }

        public virtual FilesSizeLimitSettings GetFilesSizeLimitSettings(Step1Settings settings = null)
        {
            if (settings != null)
            {
                return new FilesSizeLimitSettings(settings.SelectedSizeID, settings.SizeLimit);
            }
            else
            {
                return new FilesSizeLimitSettings();
            }
        }

        public virtual FilesNumberLimitSettings GetFilesNumberLimitSettings(Step1Settings settings = null)
        {
            if (settings != null)
            {
                return new FilesNumberLimitSettings(settings.FilesNumber);
            }
            else
            {
                return new FilesNumberLimitSettings();
            }
        }

        public virtual FilesAndFoldersLimitSettings GetFilesAndFoldersLimitSettings(Step1Settings settings = null)
        {
            if (settings != null)
            {
                return new FilesAndFoldersLimitSettings(settings.FoldersNumber, settings.FilesInFolderNumber, settings.OrganizeFiles);
            }
            else
            {
                return new FilesAndFoldersLimitSettings();
            }
        }

        public virtual void UpdateGlobalModel()
        {
            _globalModel.RandomizerWorkerSettings.FindOnlyUniqueFiles = FindOnlyUniqueFiles;
            _globalModel.RandomizerWorkerSettings.PathsFrom = PathsFrom.ToArray();
            _globalModel.RandomizerWorkerSettings.SelectedLimit = (LimitType)SelectedFilesLimit.ID;
            _globalModel.CopyWorkerSettings.SelectedLimit = (LimitType)SelectedFilesLimit.ID;

            if (_globalModel.RandomizerWorkerSettings.SelectedLimit == LimitType.FilesTotalSize)
            {
                _globalModel.RandomizerWorkerSettings.SizeLimitBytes = FilesSize.SizeLimitBytes;
            }
            else if (_globalModel.RandomizerWorkerSettings.SelectedLimit == LimitType.FilesNumber)
            {
                _globalModel.RandomizerWorkerSettings.FilesNumberLimit = FilesNumber.Number;
            }
            else if (_globalModel.RandomizerWorkerSettings.SelectedLimit == LimitType.FilesNumberPerFolder)
            {
                _globalModel.RandomizerWorkerSettings.FilesNumberPerFolderLimit = FilesAndFolders.FilesNumber;
                _globalModel.RandomizerWorkerSettings.FoldersNumberLimit = FilesAndFolders.FoldersNumber;
                _globalModel.CopyWorkerSettings.FilesPerFolderNumber = FilesAndFolders.FilesNumber;
                _globalModel.CopyWorkerSettings.FoldersNumber = FilesAndFolders.FoldersNumber;
            }
        }

        public virtual void UpdateSettings()
        {
            if (SaveSettings)
            {
                Step1Settings settings = new Step1Settings
                {
                    FindOnlyUniqueFiles = FindOnlyUniqueFiles,
                    PathsFrom = PathsFrom.ToArray(),
                    SelectedFilesLimitID = SelectedFilesLimit != null ? SelectedFilesLimit.ID : (int?)null,
                    SelectedSizeID = FilesSize.SelectedSize != null ? FilesSize.SelectedSize.ID : (int?)null,
                    SizeLimit = FilesSize.SizeLimit,
                    FilesNumber = FilesNumber.Number,
                    FilesInFolderNumber = FilesAndFolders.FilesNumber,
                    FoldersNumber = FilesAndFolders.FoldersNumber,
                    SaveSettings = SaveSettings
                };

                _settingsService.SaveSettings("Step1Settings", settings);
            }
            else
            {
                _settingsService.RemoveSettings("Step1Settings");
            }
        }

        public virtual void Initialize()
        {
            Step1Settings settings = _settingsService.LoadSettings<Step1Settings>("Step1Settings");

            if (settings != null && settings.SaveSettings)
            {
                _pathsFrom = new List<string>(settings.PathsFrom);
                _selectedFilesLimit = FilesLimitTypes.SingleOrDefault(x => x.ID == settings.SelectedFilesLimitID);

                if (_selectedFilesLimit == null)
                {
                    _selectedFilesLimit = FilesLimitTypes.First();
                }

                _findOnlyUniqueFiles = settings.FindOnlyUniqueFiles;
                _saveSettings = settings.SaveSettings;
                FilesSize = GetFilesSizeLimitSettings(settings);
                FilesNumber = GetFilesNumberLimitSettings(settings);
                FilesAndFolders = GetFilesAndFoldersLimitSettings(settings);
            }
            else
            {
                _pathsFrom = new List<string>();
                _selectedFilesLimit = FilesLimitTypes.First();
                _findOnlyUniqueFiles = true;
                _saveSettings = false;
                FilesSize = GetFilesSizeLimitSettings();
                FilesNumber = GetFilesNumberLimitSettings();
                FilesAndFolders = GetFilesAndFoldersLimitSettings();
            }
        }
    }
}