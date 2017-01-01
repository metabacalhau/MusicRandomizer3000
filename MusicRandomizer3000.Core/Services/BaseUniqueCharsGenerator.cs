using MusicRandomizer3000.Core.Services.Interfaces;

namespace MusicRandomizer3000.Core.Services
{
    public abstract class BaseUniqueCharsGenerator : IUniqueCharsGenerator
    {
        private readonly int _maxLength;

        private string _uniqueString;
        protected string UniqueString
        {
            get { return _uniqueString; }
            set { _uniqueString = value; }
        }

        protected BaseUniqueCharsGenerator(int maxLength)
        {
            _maxLength = maxLength;
        }

        public virtual string Generate()
        {
            if (_maxLength < UniqueString.Length)
            {
                UniqueString = UniqueString.Remove(_maxLength, UniqueString.Length - _maxLength);
            }

            return UniqueString;
        }
    }
}