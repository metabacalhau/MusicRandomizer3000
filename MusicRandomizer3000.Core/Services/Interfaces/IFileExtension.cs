using System.Collections;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface IFileExtension
    {
        IEnumerable AllowedExtensions { get; }
        bool Contains(string extension);
    }
}