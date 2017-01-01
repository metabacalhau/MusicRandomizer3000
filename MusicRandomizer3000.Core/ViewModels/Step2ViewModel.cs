using MusicRandomizer3000.Core.Enums;
using MusicRandomizer3000.Core.Models;
using MusicRandomizer3000.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicRandomizer3000.Core.ViewModels
{
    public class Step2ViewModel : ViewModelBase
    {
        private readonly GlobalWizardViewModel _globalModel;
        private readonly ISettingsService _settingsService;

        public Step2ViewModel(GlobalWizardViewModel globalModel, ISettingsService settingsService)
        {
            if (globalModel == null) throw new ArgumentNullException("globalModel");
            if (settingsService == null) throw new ArgumentNullException("settingsService");

            _globalModel = globalModel;
            _settingsService = settingsService;

            Initialize();
        }

        private string _pathTo;
        public virtual string PathTo
        {
            get
            {
                return _pathTo;
            }
            set
            {
                if (CheckPropertyChanged<string>("PathTo", ref _pathTo, ref value))
                {
                    FirePropertyChanged("PathTo");
                }
            }
        }

        private ComboBoxOption _selectedOnCopyOption;
        public virtual ComboBoxOption SelectedOnCopyOption
        {
            get
            {
                return _selectedOnCopyOption;
            }
            set
            {
                if (CheckPropertyChanged<ComboBoxOption>("SelectedOnCopyOption", ref _selectedOnCopyOption, ref value))
                {
                    FirePropertyChanged("SelectedOnCopyOption");
                    FirePropertyChanged("ShowSelectedOnCopyOptionDescription");
                }
            }
        }

        public virtual bool ShowSelectedOnCopyOptionDescription
        {
            get
            {
                if (SelectedOnCopyOption != null)
                {
                    if (!string.IsNullOrEmpty(SelectedOnCopyOption.Description))
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

        private ReadOnlyCollection<ComboBoxOption> _onCopyOptions = null;
        public virtual ReadOnlyCollection<ComboBoxOption> OnCopyOptions
        {
            get
            {
                if (_onCopyOptions == null)
                {
                    _onCopyOptions = new ReadOnlyCollection<ComboBoxOption>(new List<ComboBoxOption>
                    {
                        new ComboBoxOption { ID = (int)UniqueCharsPosition.None, Title = "nothing", Description = null },
                        new ComboBoxOption { ID = (int)UniqueCharsPosition.Prefix, Title = "random prefix", Description = "filename.ext → prefix_filename.ext" },
                        new ComboBoxOption { ID = (int)UniqueCharsPosition.Suffix, Title = "random suffix", Description = "filename.ext → filename_suffix.ext" }
                    });
                }

                return _onCopyOptions;
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
            _globalModel.RandomizerWorkerSettings.PathTo = PathTo;
            _globalModel.CopyWorkerSettings.PathTo = PathTo;
            _globalModel.CopyWorkerSettings.CharsPositionOnCopy = (UniqueCharsPosition)SelectedOnCopyOption.ID;
        }

        public virtual void UpdateSettings()
        {
            if (SaveSettings)
            {
                Step2Settings settings = new Step2Settings
                {
                    PathTo = PathTo,
                    OnCopyActionID = SelectedOnCopyOption.ID,
                    SaveSettings = SaveSettings
                };

                _settingsService.SaveSettings("Step2Settings", settings);
            }
            else
            {
                _settingsService.RemoveSettings("Step2Settings");
            }
        }

        public virtual void Initialize()
        {
            Step2Settings settings = _settingsService.LoadSettings<Step2Settings>("Step2Settings");

            if (settings != null && settings.SaveSettings)
            {
                _pathTo = settings.PathTo;
                _selectedOnCopyOption = OnCopyOptions.SingleOrDefault(x => x.ID == settings.OnCopyActionID);
                _saveSettings = settings.SaveSettings;
            }
            else
            {
                _selectedOnCopyOption = OnCopyOptions.First();
            }
        }
    }
}