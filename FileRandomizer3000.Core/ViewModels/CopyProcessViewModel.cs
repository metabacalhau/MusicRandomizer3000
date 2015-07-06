using FileRandomizer3000.Core.Models;
using System;

namespace FileRandomizer3000.Core.ViewModels
{
    public class CopyProcessViewModel : ViewModelBase
    {
        private readonly GlobalWizardViewModel _model;

        public CopyProcessViewModel(GlobalWizardViewModel globalModel)
        {
            if (globalModel == null) throw new ArgumentNullException("globalModel");

            _model = globalModel;
        }

        private string _stopProcessButtonText;
        public string StopProcessButtonText
        {
            get
            {
                return _stopProcessButtonText;
            }
            set
            {
                if (CheckPropertyChanged<string>("StopProcessButtonText", ref _stopProcessButtonText, ref value))
                {
                    FirePropertyChanged("StopProcessButtonText");
                }
            }
        }

        private bool _stopProcessButtonEnabled = true;
        public bool StopProcessButtonEnabled
        {
            get
            {
                return _stopProcessButtonEnabled;
            }
            set
            {
                if (CheckPropertyChanged<bool>("StopProcessButtonEnabled", ref _stopProcessButtonEnabled, ref value))
                {
                    FirePropertyChanged("StopProcessButtonEnabled");
                }
            }
        }

        private bool _stopProcessButtonVisible = true;
        public bool StopProcessButtonVisible
        {
            get
            {
                return _stopProcessButtonVisible;
            }
            set
            {
                if (CheckPropertyChanged<bool>("StopProcessButtonVisible", ref _stopProcessButtonVisible, ref value))
                {
                    FirePropertyChanged("StopProcessButtonVisible");
                }
            }
        }

        private string _goToPreviousStepButtonText;
        public string GoToPreviousStepButtonText
        {
            get
            {
                return _goToPreviousStepButtonText;
            }
            set
            {
                if (CheckPropertyChanged<string>("GoToPreviousStepButtonText", ref _goToPreviousStepButtonText, ref value))
                {
                    FirePropertyChanged("GoToPreviousStepButtonText");
                }
            }
        }

        private bool _goToPreviousStepButtonVisible = false;
        public bool GoToPreviousStepButtonVisible
        {
            get
            {
                return _goToPreviousStepButtonVisible;
            }
            set
            {
                if (CheckPropertyChanged<bool>("GoToPreviousStepButtonVisible", ref _goToPreviousStepButtonVisible, ref value))
                {
                    FirePropertyChanged("GoToPreviousStepButtonVisible");
                }
            }
        }

        private string _showCopiedFilesButtonText;
        public string ShowCopiedFilesButtonText
        {
            get
            {
                return _showCopiedFilesButtonText;
            }
            set
            {
                if (CheckPropertyChanged<string>("ShowCopiedFilesButtonText", ref _showCopiedFilesButtonText, ref value))
                {
                    FirePropertyChanged("ShowCopiedFilesButtonText");
                }
            }
        }

        private bool _showCopiedFilesButtonVisible = false;
        public bool ShowCopiedFilesButtonVisible
        {
            get
            {
                return _showCopiedFilesButtonVisible;
            }
            set
            {
                if (CheckPropertyChanged<bool>("ShowCopiedFilesButtonVisible", ref _showCopiedFilesButtonVisible, ref value))
                {
                    FirePropertyChanged("ShowCopiedFilesButtonVisible");
                }
            }
        }

        private int _progressPercentage;
        public int ProgressPercentage
        {
            get
            {
                return _progressPercentage;
            }
            set
            {
                if (CheckPropertyChanged<int>("ProgressPercentage", ref _progressPercentage, ref value))
                {
                    FirePropertyChanged("ProgressPercentage");
                }
            }
        }

        private string _progressInfoText;
        public virtual string ProgressInfoText
        {
            get
            {
                return _progressInfoText;
            }
            set
            {
                if (CheckPropertyChanged<string>("ProgressInfoText", ref _progressInfoText, ref value))
                {
                    FirePropertyChanged("ProgressInfoText");
                }
            }
        }

        public virtual CopyWorkerSettings Settings
        {
            get
            {
                return _model.CopyWorkerSettings;
            }
        }

        public string FormTitle
        {
            set
            {
                _model.FormTitle = value;
            }
        }
    }
}