namespace MusicRandomizer3000.Core.ViewModels
{
    public class FilesAndFoldersLimitSettings : ViewModelBase
    {
        public FilesAndFoldersLimitSettings()
        {
            _foldersNumber = FoldersNumberMinimum;
            _filesNumber = FilesNumberMinimum;
        }

        public FilesAndFoldersLimitSettings(int? foldersNumber, int? filesNumber, bool organizeFiles)
        {
            FoldersNumber = foldersNumber.HasValue ? foldersNumber.Value : FoldersNumberMinimum;
            FilesNumber = filesNumber.HasValue ? filesNumber.Value : FilesNumberMinimum;
            OrganizeFiles = organizeFiles;
        }

        public int FoldersNumberMinimum { get { return 1; } }
        public int FoldersNumberMaximum { get { return 6; } }

        public int FilesNumberMinimum { get { return 1; } }
        public int FilesNumberMaximum { get { return 99; } }

        private bool _organizeFiles;
        public virtual bool OrganizeFiles
        {
            get
            {
                return _organizeFiles;
            }
            set
            {
                if (CheckPropertyChanged<bool>("OrganizeFiles", ref _organizeFiles, ref value))
                {
                    FirePropertyChanged("OrganizeFiles");
                }
            }
        }

        private int _foldersNumber;
        public virtual int FoldersNumber
        {
            get
            {
                return _foldersNumber;
            }
            set
            {
                if (CheckPropertyChanged<int>("FoldersNumber", ref _foldersNumber, ref value))
                {
                    _foldersNumber = ValidateNumber(value, FoldersNumberMinimum, FoldersNumberMaximum);

                    FirePropertyChanged("FoldersNumber");
                }
            }
        }

        private int _filesNumber;
        public virtual int FilesNumber
        {
            get
            {
                return _filesNumber;
            }
            set
            {
                if (CheckPropertyChanged<int>("FilesNumber", ref _filesNumber, ref value))
                {
                    _filesNumber = ValidateNumber(value, FilesNumberMinimum, FilesNumberMaximum);

                    FirePropertyChanged("FilesNumber");
                }
            }
        }

        private int ValidateNumber(int value, int numberMinimum, int numberMaximum)
        {
            int result = numberMinimum;

            if (value >= numberMinimum && value <= numberMaximum)
            {
                result = value;
            }
            else if (value < numberMinimum)
            {
                result = numberMinimum;
            }
            else if (value > numberMaximum)
            {
                result = numberMaximum;
            }

            return result;
        }
    }
}