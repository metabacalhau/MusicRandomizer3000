using MusicRandomizer3000.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicRandomizer3000.Core.ViewModels
{
    public class FilesSizeLimitSettings : ViewModelBase
    {
        public FilesSizeLimitSettings()
        {
            Initialize();
        }

        public FilesSizeLimitSettings(int? savedSize, double? sizeLimit)
        {
            Initialize(savedSize, sizeLimit);
        }

        public void Initialize()
        {
            _selectedSize = Sizes.First();
            _sizeLimit = _selectedSize.MaxValue;
        }

        public void Initialize(int? savedSize, double? sizeLimit)
        {
            if (savedSize.HasValue)
            {
                _selectedSize = Sizes.SingleOrDefault(x => x.ID == savedSize.Value);
            }

            if (_selectedSize == null)
            {
                _selectedSize = Sizes.First();
            }

            if (sizeLimit.HasValue)
            {
                _sizeLimit = sizeLimit.Value;
            }
            else
            {
                _sizeLimit = _selectedSize.MaxValue;
            }
        }

        private List<Limit> _sizes;
        public List<Limit> Sizes
        {
            get
            {
                if (_sizes == null)
                {
                    _sizes = new List<Limit>
                    {
                        new Limit { ID = 1, Title = "Megabytes", MinValue = 1, MaxValue = 1023 },
                        new Limit { ID = 2, Title = "Gigabytes", MinValue = 1, MaxValue = 10 }
                    };
                }

                return _sizes;
            }
        }

        private string _sizeRange;
        public virtual string SizeRange
        {
            get
            {
                if (SelectedSize != null)
                {
                    _sizeRange = string.Format("({0} - {1})", SelectedSize.MinValue, SelectedSize.MaxValue);
                }

                return _sizeRange;
            }
        }

        private double _sizeLimit;
        public virtual double SizeLimit
        {
            get
            {
                return _sizeLimit;
            }
            set
            {
                if (CheckPropertyChanged<double>("SizeLimit", ref _sizeLimit, ref value))
                {
                    ValidateSizeUnitLimit(SelectedSize);
                    FirePropertyChanged("SizeLimit");
                }
            }
        }

        private Limit _selectedSize;
        public virtual Limit SelectedSize
        {
            get
            {
                return _selectedSize;
            }
            set
            {
                if (CheckPropertyChanged<Limit>("SelectedSize", ref _selectedSize, ref value))
                {
                    FirePropertyChanged("SelectedSize");
                    FirePropertyChanged("SizeRange");
                    SizeLimit = SelectedSize.MaxValue;
                }
            }
        }

        public virtual double SizeLimitBytes
        {
            get
            {
                return ConvertSizeLimitToBytes(SelectedSize);
            }
        }

        private void ValidateSizeUnitLimit(Limit selectedSize)
        {
            if (selectedSize != null)
            {
                if (SizeLimit <= selectedSize.MinValue)
                {
                    _sizeLimit = selectedSize.MinValue;
                }
                else if (SizeLimit > selectedSize.MaxValue)
                {
                    _sizeLimit = selectedSize.MaxValue;
                }
            }
        }

        private double ConvertSizeLimitToBytes(Limit selectedSize)
        {
            double sizeLimitBytes = 0;

            if (selectedSize.Title.Equals("Megabytes"))
            {
                sizeLimitBytes = SizeLimit * 1024 * 1024;
            }
            else if (selectedSize.Title.Equals("Gigabytes"))
            {
                sizeLimitBytes = SizeLimit * 1024 * 1024 * 1024;
            }

            return sizeLimitBytes;
        }
    }
}