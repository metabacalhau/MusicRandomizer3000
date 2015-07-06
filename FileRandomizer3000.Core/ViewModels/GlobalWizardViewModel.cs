using FileRandomizer3000.Core.Models;
using System.Globalization;

namespace FileRandomizer3000.Core.ViewModels
{
    public class GlobalWizardViewModel : ViewModelBase
    {
        private readonly string _titleSuffix = "";

        public GlobalWizardViewModel(string titleSuffix)
        {
            _titleSuffix = titleSuffix;
            RandomizerWorkerSettings = new RandomizerWorkerSettings();
            CopyWorkerSettings = new CopyWorkerSettings();
        }

        public virtual RandomizerWorkerSettings RandomizerWorkerSettings { get; private set; }

        public virtual CopyWorkerSettings CopyWorkerSettings { get; private set; }

        private string _formTitle;
        public virtual string FormTitle
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "{0}{1}", _formTitle, _titleSuffix);
            }
            set
            {
                if (CheckPropertyChanged<string>("FormTitle", ref _formTitle, ref value))
                {
                    FirePropertyChanged("FormTitle");
                }
            }
        }
    }
}