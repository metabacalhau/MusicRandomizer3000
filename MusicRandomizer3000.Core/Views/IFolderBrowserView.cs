using System;

namespace MusicRandomizer3000.Core.Views
{
    public interface IFolderBrowserView : IView
    {
        void PopulateFolderBrowser(string[] folders);
        event Action<string[]> OnSetSelectedFolders;
    }
}