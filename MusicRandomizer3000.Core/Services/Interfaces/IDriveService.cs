using MusicRandomizer3000.Core.Models;
using System.Collections.Generic;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface IDriveService
    {
        IEnumerable<AppDrive> GetDrives();
        AppDrive GetDrive(string path);
    }
}