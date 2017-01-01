using MusicRandomizer3000.Core.Models;
using System.Collections.Generic;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface ITagService
    {
        IEnumerable<AppFile> GetFiles(string FolderPath);
    }
}