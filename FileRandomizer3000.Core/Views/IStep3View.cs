using FileRandomizer3000.Core.ViewModels;
using System;

namespace FileRandomizer3000.Core.Views
{
    public interface IStep3View : IView
    {
        event Action OnBeforeViewShown;
        event Action OnPreviousStepClick;
        event Action OnNextStepClick;
        void Initialize(Step3ViewModel model);
        void SetActiveView();
        bool IsInitialized { get; }
    }
}