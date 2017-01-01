using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using MusicRandomizer3000.Core.Views;
using System.Collections.Generic;

namespace MusicRandomizer3000.Core.Presenters
{
    public class FolderBrowserPresenter : BaseWizardPresenter<IFolderBrowserView, List<string>>
    {
        private List<string> folders;

        public FolderBrowserPresenter(IApplicationController controller, IFolderBrowserView view) : base(controller, view)
        {
            View.OnSetSelectedFolders += OnSetSelectedFolders;
        }

        private void OnSetSelectedFolders(string[] folders)
        {
            this.folders.Clear();

            if (folders != null)
            {
                foreach (var folder in folders)
                {
                    this.folders.Add(folder);
                }
            }
        }

        public override void Run(List<string> folders)
        {
            this.folders = folders;
            View.PopulateFolderBrowser(folders.ToArray());
            View.Show();
        }
    }
}