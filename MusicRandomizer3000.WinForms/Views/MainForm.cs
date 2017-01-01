using MusicRandomizer3000.Core.Views;
using MusicRandomizer3000.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace MusicRandomizer3000.WinForms.Views
{
    public partial class MainForm : Form, IMainFormView
    {
        private readonly ApplicationContext _context;

        public MainForm(ApplicationContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        public ReadOnlyCollection<IView> Views
        {
            get
            {
                List<IView> views = new List<IView>();

                foreach (Control viewControl in Controls)
                {
                    views.Add((IView)viewControl);
                }

                return new ReadOnlyCollection<IView>(views);
            }
        }

        private IView _activeView;
        public IView ActiveView
        {
            get
            {
                return _activeView;
            }
        }

        public event Action<IView, IView> OnActiveViewChanged;

        public void AddView(IView view)
        {
            if (view == null) throw new ArgumentNullException("view");

            if (view is Control)
            {
                Control viewControl = (Control)view;

                int index = Controls.IndexOf(viewControl);

                if (index == -1)
                {
                    Controls.Add(viewControl);
                }
                else
                {
                    throw new Exception(string.Format("Cannot add view with the key: {0}. It already exists in collection.", view.GetType().Name));
                }
            }
            else
            {
                throw new ArgumentException("The View is not of Control type and cannot be added.");
            }
        }

        public void RemoveView(IView view)
        {
            if (view == null) throw new ArgumentNullException("view");

            if (view is Control)
            {
                Control viewControl = (Control)view;

                int index = Controls.IndexOf(viewControl);

                if (index > -1)
                {
                    Controls.RemoveAt(index);

                    if (viewControl == _activeView)
                    {
                        if (OnActiveViewChanged != null)
                        {
                            OnActiveViewChanged(_activeView, null);
                        }

                        _activeView = null;
                    }
                }
            }
            else
            {
                throw new ArgumentException("The View is not of Control type and cannot be added.");
            }
        }

        public void SetActiveView(IView view)
        {
            if (view == null) throw new ArgumentNullException("view");

            if (OnActiveViewChanged != null)
            {
                OnActiveViewChanged(_activeView, view);
            }

            _activeView = view;
        }

        public void AlertErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        public new void Show()
        {
            _context.MainForm = this;
            Application.Run(_context);
        }

        public void Initialize(GlobalWizardViewModel model)
        {
            if (model == null)
            {
                AlertErrorMessage("Model is null");
                return;
            }

            InitializeComponent();

            DataBindings.Add("Text", model, "FormTitle", false, DataSourceUpdateMode.OnPropertyChanged);

            Show();
        }
    }
}
