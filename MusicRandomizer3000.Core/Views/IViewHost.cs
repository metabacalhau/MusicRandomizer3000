using System;
using System.Collections.ObjectModel;

namespace MusicRandomizer3000.Core.Views
{
    public interface IViewHost
    {
        ReadOnlyCollection<IView> Views { get; }

        IView ActiveView { get; }

        event Action<IView, IView> OnActiveViewChanged;

        void AddView(IView view);

        void RemoveView(IView view);

        void SetActiveView(IView view);
    }
}