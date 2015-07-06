using FileRandomizer3000.Core;
using FileRandomizer3000.Core.Infrastructure;
using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Presenters;
using FileRandomizer3000.Core.Services;
using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.Utilities;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Core.Views;
using FileRandomizer3000.WinForms.Properties;
using FileRandomizer3000.WinForms.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace FileRandomizer3000.WinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var controller = new ApplicationController(new StructureMapAdapter())
                .RegisterService<IFileSystem, FileSystem>()
                .RegisterService<IDriveService, DriveService>()
                .RegisterService<IFolderService, FolderService>()
                .RegisterService<ITraverseService, TraverseService>()
                .RegisterService<IFileHelper, FileHelper>()
                .RegisterService<IFileService, FileService>()
                .RegisterService<ITagService, TagService>()
                .RegisterService<IEqualityComparer<AppFile>, FileNameSizeComparer>()
                .RegisterService<IBackgroundWorkerFactory, BackgroundWorkerFactory>()
                .RegisterService<ICopyWorker, CopyWorker>()
                .RegisterService<IRandomizerWorker, RandomizerWorker>()
                .RegisterService<ISettingsService, SettingsService>()
                .RegisterService<IContext, SynchronizationContextAdapter>()
                .RegisterService<IProcessWrapper, ProcessWrapper>()
                .RegisterInstance<IFileExtension>(new DefaultMusicExtensions())
                .RegisterInstance<IUniqueCharsGenerator>(new GuidCharactersGenerator(7))
                .RegisterInstance(new ApplicationContext())
                .RegisterSingletonView<IMainFormView, MainForm>()
                .RegisterSingletonView<IStep1View, Step1View>()
                .RegisterSingletonView<IStep2View, Step2View>()
                .RegisterSingletonView<IStep3View, Step3View>()
                .RegisterView<IRandomizationProcessView, RandomizationProcessView>()
                .RegisterView<ICopyProcessView, CopyProcessView>()
                .RegisterInstance(new GlobalWizardViewModel(" - FileRandomizer3000"))
                .RegisterInstance<SettingsBase>(Settings.Default)
                .RegisterService<IBackgroundWorker, BackgroundWorkerAsync>()
                .RegisterSingletonService<IMainFormViewHost, MainFormViewHost>()
                .RegisterSingletonService<Step1ViewModel, Step1ViewModel>()
                .RegisterSingletonService<Step2ViewModel, Step2ViewModel>()
                .RegisterSingletonService<Step3ViewModel, Step3ViewModel>()
                .RegisterSingletonService<RandomizationProcessViewModel, RandomizationProcessViewModel>()
                .RegisterSingletonService<CopyProcessViewModel, CopyProcessViewModel>();

            controller.Run<MainFormPresenter>();
        }
    }
}