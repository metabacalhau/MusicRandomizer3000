using MusicRandomizer3000.Core.Services.Interfaces;
using System.Collections;
using System.Linq;

namespace MusicRandomizer3000.Core.Services
{
    public abstract class FileExtensionDecorator : IFileExtension
    {
        private string[] _allowed = null;
        protected string[] Allowed
        {
            get { return _allowed; }
            set { _allowed = value; }
        }

        private IFileExtension _additional = null;
        protected IFileExtension Additional
        {
            get { return _additional; }
            set { _additional = value; }
        }

        protected FileExtensionDecorator(IFileExtension additionalExtensions)
        {
            Additional = additionalExtensions;
        }

        public virtual IEnumerable AllowedExtensions
        {
            get
            {
                return Allowed;
            }
        }

        public virtual bool Contains(string extension)
        {
            if (Additional != null && Additional.Contains(extension))
            {
                return true;
            }
            else
            {
                return Allowed.Contains(extension);
            }
        }
    }
}