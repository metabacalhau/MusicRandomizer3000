using FileRandomizer3000.Core.ViewModels;
using System;

namespace FileRandomizer3000.Core.Views
{
    public interface IRandomizationProcessView : IView
    {
        event Action OnBeforeViewShown;
        event Action OnRandomizationStopped;
        event Action OnRandomizationAborted;
        void Initialize(RandomizationProcessViewModel model);
        void StartRandomization();
        void StopRandomization();
        void RandomizationAborted();
        void NothingToCopy();
        void AlertErrorMessage(string errorMessage);
        void SetActiveView();
    }
}