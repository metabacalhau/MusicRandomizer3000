using FileRandomizer3000.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileRandomizer3000.Core.Infrastructure.Interfaces;

namespace FileRandomizer3000.Core.Presenters
{
    public class FolderBrowserPresenter : BaseWizardPresenter<IFolderBrowserView, string[]>
    {
        private string[] folders = null;

        public FolderBrowserPresenter(IApplicationController controller, IFolderBrowserView view) : base(controller, view)
        {
            View.OnSetSelectedFolders += OnSetSelectedFolders;
        }

        private void OnSetSelectedFolders(string[] folders)
        {
            this.folders = folders;
        }

        public override void Run(string[] folders)
        {
            this.folders = folders;
            View.PopulateFolderBrowser(this.folders);
            View.Show();
        }
    }
}