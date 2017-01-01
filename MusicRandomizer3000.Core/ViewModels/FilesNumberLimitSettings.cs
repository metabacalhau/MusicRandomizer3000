namespace MusicRandomizer3000.Core.ViewModels
{
    public class FilesNumberLimitSettings : ViewModelBase
    {
        public FilesNumberLimitSettings()
        {
            Number = Minimum;
        }

        public FilesNumberLimitSettings(int? savedNumber)
        {
            Number = savedNumber.HasValue ? savedNumber.Value : Minimum;
        }

        public int Minimum { get { return 1; } }

        public int Maximum { get { return 10000; } }

        private int _number;
        public virtual int Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (CheckPropertyChanged<int>("Number", ref _number, ref value))
                {
                    _number = ValidateNumber(value);

                    FirePropertyChanged("Number");
                }
            }
        }

        public string Range
        {
            get
            {
                return string.Format("({0} - {1})", Minimum, Maximum);
            }
        }

        private int ValidateNumber(int? newValue)
        {
            int result = 0;

            if (newValue >= Minimum && newValue <= Maximum)
            {
                result = newValue.Value;
            }
            else if (newValue < Minimum)
            {
                result = Minimum;
            }
            else if (newValue > Maximum)
            {
                result = Maximum;
            }

            return result;
        }
    }
}