using FileRandomizer3000.Core.ViewModels;
using System;

namespace FileRandomizer3000.Core.Views
{
    public interface ICopyProcessView : IView
    {
        event Action OnBeforeViewShown;
        event Action OnCopyStopped;
        event Action OnPreviousStepClick;
        event Action OnShowCopiedFiles;
        void Initialize(CopyProcessViewModel model);
        void StartCopy();
        void StopCopy();
        void CopyStarted();
        void CopyAborted();
        void CopyFinished();
        void ReportProgress(int progressPercentage);
        void AlertErrorMessage(string errorMessage);
        void SetActiveView();
    }
}