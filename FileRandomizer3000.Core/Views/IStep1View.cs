using FileRandomizer3000.Core.ViewModels;
using System;

namespace FileRandomizer3000.Core.Views
{
    public interface IStep1View : IView
    {
        event Action OnNextStepClick;
        event Action OnBeforeViewShown;
        event Action OnOrganizeFilesDescriptionClick;
        void Initialize(Step1ViewModel model);
        void AlertErrorMessage(string errorMessage);
        void ShowPathFromIsEmptyError(string errorMessage);
        void ShowPathFromIsInaccessible(string errorMessage);
        void HidePathFromError();
        void ShowOrganizeFilesDescription(string description);
        void HideOrganizeFilesDescription();
        void SetActiveView();
        bool IsInitialized { get; }
    }
}