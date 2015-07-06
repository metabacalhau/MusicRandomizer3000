using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FileRandomizer3000.Core.Enums;

namespace FileRandomizer3000.Core.ViewModels
{
    public class Step3ViewModel : ViewModelBase
    {
        private readonly GlobalWizardViewModel _globalModel;
        private readonly ISettingsService _settingsService;

        public Step3ViewModel(GlobalWizardViewModel globalModel, ISettingsService settingsService)
        {
            if (globalModel == null) throw new ArgumentNullException("globalModel");
            if (settingsService == null) throw new ArgumentNullException("settingsService");

            _globalModel = globalModel;
            _settingsService = settingsService;

            Initialize();
        }

        private ReadOnlyCollection<ComboBoxOption> _onDuplicateOptions = null;
        public virtual ReadOnlyCollection<ComboBoxOption> OnDuplicateOptions
        {
            get
            {
                if (_onDuplicateOptions == null)
                {
                    _onDuplicateOptions = new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>
                    {
                        new ComboBoxOption { ID = 1, Title = "skip" },
                        new ComboBoxOption { ID = 2, Title = "overwrite" },
                        new ComboBoxOption { ID = 3, Title = "add random prefix", Description = "filename.ext → prefix_filename.ext" },
                        new ComboBoxOption { ID = 4, Title = "add random suffix", Description = "filename.ext → filename_suffix.ext" }
                    });
                }

                return _onDuplicateOptions;
            }
        }

        private ComboBoxOption _selectedOnDuplicateOption;
        public virtual ComboBoxOption SelectedOnDuplicateOption
        {
            get
            {
                return _selectedOnDuplicateOption;
            }
            set
            {
                if (CheckPropertyChanged<ComboBoxOption>("SelectedOnDuplicateOption", ref _selectedOnDuplicateOption, ref value))
                {
                    FirePropertyChanged("SelectedOnDuplicateOption");
                    FirePropertyChanged("ShowSelectedOnDuplicateOptionDescription");
                }
            }
        }

        public virtual bool ShowSelectedOnDuplicateOptionDescription
        {
            get
            {
                if (SelectedOnDuplicateOption != null)
                {
                    if (!string.IsNullOrEmpty(SelectedOnDuplicateOption.Description))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        private ReadOnlyCollection<ComboBoxOption> _sortOptions = null;
        public virtual ReadOnlyCollection<ComboBoxOption> SortOptions
        {
            get
            {
                if (_sortOptions == null)
                {
                    _sortOptions = new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>
                    {
                        new ComboBoxOption { ID = (int)SortOrder.Ascending, Title = "ascending" },
                        new ComboBoxOption { ID = (int)SortOrder.Descending, Title = "descending" }
                    });
                }

                return _sortOptions;
            }
        }

        private ComboBoxOption _selectedSortOption;
        public virtual ComboBoxOption SelectedSortOption
        {
            get
            {
                return _selectedSortOption;
            }
            set
            {
                if (CheckPropertyChanged<ComboBoxOption>("SelectedSortOption", ref _selectedSortOption, ref value))
                {
                    FirePropertyChanged("SelectedSortOption");
                }
            }
        }

        private bool _showRandomizedFiles;
        public virtual bool ShowRandomizedFiles
        {
            get
            {
                return _showRandomizedFiles;
            }
            set
            {
                if (CheckPropertyChanged<bool>("ShowRandomizedFiles", ref _showRandomizedFiles, ref value))
                {
                    FirePropertyChanged("ShowRandomizedFiles");
                    FirePropertyChanged("NextButtonText");
                }
            }
        }

        private bool _showRandomizedFilesVisible;
        public virtual bool ShowRandomizedFilesVisible
        {
            get
            {
                return _showRandomizedFilesVisible;
            }
            set
            {
                if (CheckPropertyChanged<bool>("ShowRandomizedFilesVisible", ref _showRandomizedFilesVisible, ref value))
                {
                    FirePropertyChanged("ShowRandomizedFilesVisible");
                }
            }
        }

        private string _nextButtonText;
        public virtual string NextButtonText
        {
            get
            {
                if (ShowRandomizedFiles)
                {
                    _nextButtonText = "Randomize and Show";
                }
                else
                {
                    _nextButtonText = "Randomize and Copy";
                }

                return _nextButtonText;
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

        public virtual void UpdateGlobalModel()
        {
            _globalModel.RandomizerWorkerSettings.OnDuplicateDoNotCopy = SelectedOnDuplicateOption.ID == 1;
            _globalModel.CopyWorkerSettings.OnDuplicateDoNotCopy = SelectedOnDuplicateOption.ID == 1;
            _globalModel.CopyWorkerSettings.OnDuplicateOverwrite = SelectedOnDuplicateOption.ID == 2;
            _globalModel.CopyWorkerSettings.OnDuplicateAddPrefix = SelectedOnDuplicateOption.ID == 3;
            _globalModel.CopyWorkerSettings.OnDuplicateAddSuffix = SelectedOnDuplicateOption.ID == 4;

            UniqueCharsPosition charsPositionOnDuplicate;

            if (_globalModel.CopyWorkerSettings.OnDuplicateAddSuffix)
            {
                charsPositionOnDuplicate = UniqueCharsPosition.Suffix;
            }
            else if (_globalModel.CopyWorkerSettings.OnDuplicateAddPrefix)
            {
                charsPositionOnDuplicate = UniqueCharsPosition.Prefix;
            }
            else
            {
                charsPositionOnDuplicate = UniqueCharsPosition.None;
            }

            _globalModel.CopyWorkerSettings.CharsPositionOnDuplicate = charsPositionOnDuplicate;
            _globalModel.CopyWorkerSettings.SelectedSortOrder = (SortOrder)SelectedSortOption.ID;
        }

        public virtual void UpdateSettings()
        {
            if (SaveSettings)
            {
                Step3Settings settings = new Step3Settings
                {
                    OnDuplicateOptionID = SelectedOnDuplicateOption.ID,
                    SortOptionID = SelectedSortOption.ID,
                    ShowRandomizedFiles = ShowRandomizedFiles,
                    SaveSettings = SaveSettings
                };

                _settingsService.SaveSettings("Step3Settings", settings);
            }
            else
            {
                _settingsService.RemoveSettings("Step3Settings");
            }
        }

        public virtual void Initialize()
        {
            Step3Settings settings = _settingsService.LoadSettings<Step3Settings>("Step3Settings");

            if (settings != null)
            {
                _selectedOnDuplicateOption = OnDuplicateOptions.SingleOrDefault(x => x.ID == settings.OnDuplicateOptionID);
                _selectedSortOption = SortOptions.SingleOrDefault(x => x.ID == settings.SortOptionID);

                if (_selectedSortOption == null)
                {
                    _selectedSortOption = SortOptions.First();
                }

                _showRandomizedFiles = settings.ShowRandomizedFiles;
                _saveSettings = settings.SaveSettings;
            }
            else
            {
                _selectedOnDuplicateOption = OnDuplicateOptions.First();
                _selectedSortOption = SortOptions.First();
            }
        }
    }
}