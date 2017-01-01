using MusicRandomizer3000.Core.Views;
using Raccoom.Windows.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MusicRandomizer3000.WinForms.Views
{
    public partial class FolderBrowser : Form, IFolderBrowserView
    {
        public FolderBrowser()
        {
            InitializeComponent();

            var provider = new TreeStrategyFolderBrowserProvider();
            provider.RootFolder = Environment.SpecialFolder.MyComputer;

            treeViewFolderBrowser.ShowLines = true;
            treeViewFolderBrowser.ShowPlusMinus = true;
            treeViewFolderBrowser.ShowRootLines = true;
            treeViewFolderBrowser.DataSource = provider;
            treeViewFolderBrowser.CheckBoxBehaviorMode = CheckBoxBehaviorMode.RecursiveChecked;

            this.FormClosing += delegate { Invoke(SetSelectedFolders); };
        }

        public event Action<string[]> OnSetSelectedFolders;

        void IView.Show()
        {
            ShowDialog();
        }

        private void SetSelectedFolders()
        {
            if (OnSetSelectedFolders != null)
            {
                var selectedDirectories = new string[] { };

                if (treeViewFolderBrowser.SelectedDirectories != null)
                {
                    selectedDirectories = treeViewFolderBrowser.SelectedDirectories.ToArray();
                }

                OnSetSelectedFolders(selectedDirectories);
            }
        }

        public void PopulateFolderBrowser(string[] folders)
        {
            if (folders != null)
            {
                foreach (var folder in folders)
                {
                    treeViewFolderBrowser.SelectedDirectories.Add(folder);
                }

                treeViewFolderBrowser.Populate();
            }
        }

        private void Invoke(Action action)
        {
            if (action != null) action();
        }
    }
}
