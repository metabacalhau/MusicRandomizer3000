using MusicRandomizer3000.Core.Services.Interfaces;
using MusicRandomizer3000.Core.Models;
using System.Collections.Generic;
using System.IO;

namespace MusicRandomizer3000.Core.Services
{
    public class DriveService : IDriveService
    {
        public IEnumerable<AppDrive> GetDrives()
        {
            foreach (DriveInfo systemDrive in DriveInfo.GetDrives())
            {
                if (systemDrive.IsReady)
                {
                    AppDrive drive = new AppDrive();
                    drive.AvailableFreeSpace = systemDrive.AvailableFreeSpace;
                    drive.DriveName = systemDrive.Name;
                    yield return drive;
                }
            }
        }

        public AppDrive GetDrive(string path)
        {
            DriveInfo systemDrive = new DriveInfo(System.IO.Path.GetPathRoot(path));

            AppDrive drive = new AppDrive();
            drive.DriveName = systemDrive.Name;
            drive.AvailableFreeSpace = systemDrive.AvailableFreeSpace;

            return drive;
        }
    }
}
