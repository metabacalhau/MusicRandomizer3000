using MusicRandomizer3000.Core.ViewModels;

namespace MusicRandomizer3000.Core.Views
{
    public interface IMainFormView : IView, IViewHost
    {
        void Initialize(GlobalWizardViewModel model);
        void AlertErrorMessage(string errorMessage);
    }
}