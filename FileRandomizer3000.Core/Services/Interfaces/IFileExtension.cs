using System.Collections;

namespace FileRandomizer3000.Core.Services.Interfaces
{
    public interface IFileExtension
    {
        IEnumerable AllowedExtensions { get; }
        bool Contains(string extension);
    }
}