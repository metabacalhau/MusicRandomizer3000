using FileRandomizer3000.Core.Views;
using Raccoom.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileRandomizer3000.WinForms.Views
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
        }

        public event Action<string[]> OnSetSelectedFolders;

        void IView.Show()
        {
            ShowDialog();
        }

        void IView.Close()
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

            base.Close();
        }

        public void PopulateFolderBrowser(string[] folders)
        {
            if (folders != null)
            {
                foreach(var folder in folders)
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
