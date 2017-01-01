using MusicRandomizer3000.Core.Services.Interfaces;
using System;
using System.Diagnostics;

namespace MusicRandomizer3000.Core.Services
{
    public class ProcessWrapper : IProcessWrapper
    {
        private readonly IFolderService folderService;

        public ProcessWrapper(IFolderService folderService)
        {
            this.folderService = folderService;
        }

        public void Start(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath)) throw new ArgumentNullException("fileName");

            if (folderService.IsAccessible(folderPath))
            {
                Process.Start(folderPath);
            }
        }
    }
}