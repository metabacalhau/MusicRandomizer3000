using MusicRandomizer3000.Core.ViewModels;
using System;

namespace MusicRandomizer3000.Core.Views
{
    public interface IStep2View : IView
    {
        event Action OnBeforeViewShown;
        event Action OnPreviousStepClick;
        event Action OnNextStepClick;
        event Action OnCopyActionsDescriptionClick;
        void ShowPathToIsEmptyError(string errorMessage);
        void ShowPathToIsInaccessible(string errorMessage);
        void HidePathToError();
        void ShowOnCopyAddDescription(string description);
        void HideOnCopyAddDescription();
        void Initialize(Step2ViewModel model);
        void SetActiveView();
        bool IsInitialized { get; }
    }
}