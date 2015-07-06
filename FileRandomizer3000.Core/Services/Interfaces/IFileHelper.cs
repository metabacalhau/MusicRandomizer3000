using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Models;
using System.Collections.Generic;

namespace FileRandomizer3000.Core.Services.Interfaces
{
    public interface IFileHelper
    {
        string FileSizeFormatter(double size);
        double FileSizeConverter(double size, FileSizeUnit unit);
        string GenerateUniqueFileName(AppFile file, IUniqueCharsGenerator generator, UniqueCharsPosition position);
        AppFile GetRandomFile(IEnumerable<AppFile> files);
    }
}