using MusicRandomizer3000.Core.Models;
using System;

namespace MusicRandomizer3000.Core.ViewModels
{
    public class RandomizationProcessViewModel : ViewModelBase
    {
        private readonly GlobalWizardViewModel _globalModel;

        public RandomizationProcessViewModel(GlobalWizardViewModel globalModel)
        {
            if (globalModel == null) throw new ArgumentNullException("globalModel");

            _globalModel = globalModel;
        }

        private string _stopProcessButtonText;
        public virtual string StopProcessButtonText
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
        public virtual bool StopProcessButtonEnabled
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
        public virtual bool StopProcessButtonVisible
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
        public virtual string GoToPreviousStepButtonText
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
        public virtual bool GoToPreviousStepButtonVisible
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

        private string _infoText;
        public virtual string InfoText
        {
            get
            {
                return _infoText;
            }
            set
            {
                if (CheckPropertyChanged<string>("InfoText", ref _infoText, ref value))
                {
                    FirePropertyChanged("InfoText");
                }
            }
        }

        private bool _infoVisible;
        public virtual bool InfoVisible
        {
            get
            {
                return _infoVisible;
            }
            set
            {
                if (CheckPropertyChanged<bool>("InfoVisible", ref _infoVisible, ref value))
                {
                    FirePropertyChanged("InfoVisible");
                }
            }
        }

        private bool _spinnerImageVisible;
        public virtual bool SpinnerImageVisible
        {
            get
            {
                return _spinnerImageVisible;
            }
            set
            {
                if (CheckPropertyChanged<bool>("SpinnerImageVisible", ref _spinnerImageVisible, ref value))
                {
                    FirePropertyChanged("SpinnerImageVisible");
                }
            }
        }

        public RandomizerWorkerSettings Settings
        {
            get
            {
                return _globalModel.RandomizerWorkerSettings;
            }
        }

        public string FormTitle
        {
            set
            {
                _globalModel.FormTitle = value;
            }
        }
    }
}