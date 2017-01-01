using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using MusicRandomizer3000.Core.Models;
using MusicRandomizer3000.Core.Services.Interfaces;
using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicRandomizer3000.Core.Presenters
{
    public class RandomizationProcessPresenter : BaseWizardPresenter<IRandomizationProcessView>
    {
        private readonly IRandomizerWorker _randomizerWorker;
        private RandomizationProcessViewModel _model;
        private List<AppFile> _filesToCopy;

        public RandomizationProcessPresenter(IApplicationController controller, IRandomizationProcessView view, RandomizationProcessViewModel model, IRandomizerWorker randomizerWorker)
            : base(controller, view)
        {
            if (model == null) throw new ArgumentNullException("model");
            if (randomizerWorker == null) throw new ArgumentNullException("randomizerWorker");

            _model = model;
            _randomizerWorker = randomizerWorker;
        }

        public override void Run()
        {
            View.OnBeforeViewShown += OnBeforeViewShownHandler;
            View.OnRandomizationStopped += OnRandomizationStopped;
            View.OnRandomizationAborted += OnRandomizationAborted;
            View.Initialize(_model);
            View.SetActiveView();
        }

        private void OnBeforeViewShownHandler()
        {
            _randomizerWorker.Run(_model.Settings, OnFinished, OnFailed, OnCancelled);
            View.StartRandomization();
        }

        private void OnRandomizationStopped()
        {
            _randomizerWorker.Cancel();
            View.StopRandomization();
        }

        private void OnRandomizationAborted()
        {
            Controller.RunSingleton<Step3Presenter>();
        }

        public void OnFinished(List<AppFile> files)
        {
            if (files != null && files.Any())
            {
                Controller.Run<CopyProcessPresenter, List<AppFile>>(files);
            }
            else
            {
                View.NothingToCopy();
            }
        }

        public void OnFailed(string errorMessage)
        {
            View.AlertErrorMessage(errorMessage);
        }

        public void OnCancelled()
        {
            View.RandomizationAborted();
        }
    }
}