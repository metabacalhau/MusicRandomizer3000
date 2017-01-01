using MusicRandomizer3000.Core.Enums;
using MusicRandomizer3000.Core.Models;
using System.Collections.Generic;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface IFileHelper
    {
        string FileSizeFormatter(double size);
        double FileSizeConverter(double size, FileSizeUnit unit);
        string GenerateUniqueFileName(AppFile file, IUniqueCharsGenerator generator, UniqueCharsPosition position);
        AppFile GetRandomFile(IEnumerable<AppFile> files);
    }
}