namespace MusicRandomizer3000.Core.Views
{
    public class MainFormViewHost : IMainFormViewHost
    {
        private readonly IMainFormView _view;

        public MainFormViewHost(IMainFormView view)
        {
            _view = view;
        }

        public void ActivateView(IView view)
        {
            _view.SetActiveView(view);
        }
    }
}