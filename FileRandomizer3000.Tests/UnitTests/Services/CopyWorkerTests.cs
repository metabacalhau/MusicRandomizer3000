using FileRandomizer3000.Core;
using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services;
using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class CopyWorkerTests
    {
        Mock<IFileSystem> fileSystemMock = null;
        Mock<IFileHelper> fileHelperMock = null;
        Mock<IUniqueCharsGenerator> generatorMock = null;
        Mock<CopyWorkerSettings> settingsMock = null;
        ContextStub _uiContextStub = null;
        Mock<IBackgroundWorker> _backgroundWorkerMock = null;
        CopyWorker _worker = null;
        CopyWorkerParameters _parameters = null;

        [SetUp]
        public void SetUp()
        {
            fileSystemMock = new Mock<IFileSystem>();
            fileHelperMock = new Mock<IFileHelper>();
            generatorMock = new Mock<IUniqueCharsGenerator>();
            settingsMock = new Mock<CopyWorkerSettings>();
            _uiContextStub = new ContextStub();
            _backgroundWorkerMock = new Mock<IBackgroundWorker>();
            _worker = new CopyWorker(fileSystemMock.Object, fileHelperMock.Object, _uiContextStub, _backgroundWorkerMock.Object, generatorMock.Object);
            _parameters = new CopyWorkerParameters
            {
                Settings = settingsMock.Object,
                OnStarted = () => { },
                OnFileChanged = (f) => { },
                OnProgressChanged = (i) => { },
                OnFinished = () => { },
                OnFailed = (s) => { },
                OnCancelled = () => { }
            };
        }

        private List<AppFile> InitializeFiles()
        {
            List<AppFile> filesToCopy = new List<AppFile>();
            filesToCopy.Add(new AppFile { FileNameFull = "file1.exe" });
            filesToCopy.Add(new AppFile { FileNameFull = "file2.exe" });
            filesToCopy.Add(new AppFile { FileNameFull = "file3.exe" });
            filesToCopy.Add(new AppFile { FileNameFull = "file4.exe" });
            filesToCopy.Add(new AppFile { FileNameFull = "file5.exe" });

            return filesToCopy;
        }

        [Test]
        public void CopyWorker_SupplyNullFileSystemArgument_ThrowsFileSystemArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new CopyWorker(null, null, null, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("fileSystem"));
        }

        [Test]
        public void CopyWorker_SupplyNullFileHelperArgument_ThrowsFileHelperArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new CopyWorker(fileSystemMock.Object, null, null, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("fileHelper"));
        }

        [Test]
        public void CopyWorker_SupplyNullUIContextArgument_ThrowsUIContextArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new CopyWorker(fileSystemMock.Object, fileHelperMock.Object, null, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("uiContext"));
        }

        [Test]
        public void CopyWorker_SupplyNullBackgroundWorkerArgument_ThrowsBackgroundWorkerArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new CopyWorker(fileSystemMock.Object, fileHelperMock.Object, _uiContextStub, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("backgroundWorker"));
        }

        [Test]
        public void CopyWorker_SupplyNullUniqueCharsGeneratorArgument_ThrowsUniqueCharsGeneratorArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new CopyWorker(fileSystemMock.Object, fileHelperMock.Object, _uiContextStub, _backgroundWorkerMock.Object, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("charsGenerator"));
        }

        [Test]
        public void Run_SupplyNullParameters_ThrowsParametersArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => _worker.Run(null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("parameters"));
        }

        [Test]
        public void Run_SupplyNullSettingsArgument_ThrowsSettingsArgumentNullException()
        {
            // arrange
            _parameters.Settings = null;

            TestDelegate testDelegate = () => _worker.Run(_parameters);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("settings"));
        }

        [Test]
        public void Run_SupplyNullOnStartedCallback_ThrowsOnStartedArgumentNullException()
        {
            // arrange
            _parameters.OnStarted = null;

            TestDelegate testDelegate = () => _worker.Run(_parameters);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onStarted"));
        }

        [Test]
        public void Run_SupplyNullOnFileChangedCallback_ThrowsOnFileChangedArgumentNullException()
        {
            // arrange
            _parameters.OnFileChanged = null;

            TestDelegate testDelegate = () => _worker.Run(_parameters);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onFileChanged"));
        }

        [Test]
        public void Run_SupplyNullOnProgressChangedCallback_ThrowsOnProgressChangedArgumentNullException()
        {
            // arrange
            _parameters.OnProgressChanged = null;

            TestDelegate testDelegate = () => _worker.Run(_parameters);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onProgressChanged"));
        }

        [Test]
        public void Run_SupplyNullOnFinishedCallback_ThrowsOnFinishedArgumentNullException()
        {
            // arrange
            _parameters.OnFinished = null;

            TestDelegate testDelegate = () => _worker.Run(_parameters);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onFinished"));
        }

        [Test]
        public void Run_SupplyNullOnFailedCallback_ThrowsOnFailedArgumentNullException()
        {
            // arrange
            _parameters.OnFailed = null;

            TestDelegate testDelegate = () => _worker.Run(_parameters);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onFailed"));
        }

        [Test]
        public void Run_SupplyNullOnCancelledCallback_ThrowsOnCancelledArgumentNullException()
        {
            // arrange
            _parameters.OnCancelled = null;

            TestDelegate testDelegate = () => _worker.Run(_parameters);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onCancelled"));
        }

        [Test]
        public void Run_BackgroundWorkerMustSupportProgressReport()
        {
            // arrange
            _backgroundWorkerMock.SetupSet(x => x.WorkerReportsProgress).Callback(x => Assert.IsTrue(x));

            // act
            _worker.Run(_parameters);
        }

        [Test]
        public void Run_BackgroundWorkerMustSupportCancellation()
        {
            // arrange
            _backgroundWorkerMock.SetupSet(x => x.WorkerSupportsCancellation).Callback(x => Assert.IsTrue(x));

            // act
            _worker.Run(_parameters);
        }

        [Test]
        public void Run_CancellationIsPending_WorkerStopsAndDoesntIterateOverFiles()
        {
            // arrange
            _backgroundWorkerMock.SetupGet(x => x.CancellationPending).Returns(true);

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            settingsMock.VerifyGet(x => x.FilesToCopy, Times.Never());
        }

        [Test]
        public void Run_CancellationIsPending_WorkerStopsAfterThirdFileHasCopiedAndDoesntContinueCopy()
        {
            // arrange
            List<AppFile> filesToCopy = InitializeFiles();

            int fileIndex = 0;

            fileSystemMock.Setup(s => s.CopyTo(It.IsAny<string>(), It.IsAny<string>())).Callback(() =>
            {
                ++fileIndex;

                if (fileIndex == 3)
                {
                    _backgroundWorkerMock.SetupGet(x => x.CancellationPending).Returns(true);
                }
            });

            settingsMock.SetupGet(s => s.FilesToCopy).Returns(filesToCopy);

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            fileSystemMock.Verify(x => x.CopyTo(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        public void Run_SupplyFiveFilesToCopy_BackgroundWorkerOnProgressCallsOnFileChangedCallbackShouldBeCalledForEachFile()
        {
            // arrange
            List<AppFile> calledFiles = new List<AppFile>();
            List<AppFile> filesToCopy = InitializeFiles();

            settingsMock.SetupGet(x => x.FilesToCopy).Returns(filesToCopy);

            _backgroundWorkerMock
                .Setup(x => x.ReportProgress(It.IsAny<int>(), It.IsAny<object>()))
                .Callback<int, object>((percentProgress, argument) =>
                {
                    _backgroundWorkerMock.Raise(y => y.OnProgressChanged += null, null, new System.ComponentModel.ProgressChangedEventArgs(percentProgress, argument));
                });

            _parameters.OnFileChanged = (file) => calledFiles.Add(file);

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            CollectionAssert.AreEqual(filesToCopy, calledFiles);
        }

        [TestCase(0, new int[] { })]
        [TestCase(1, new int[] { 100 })]
        [TestCase(2, new int[] { 50, 100 })]
        [TestCase(3, new int[] { 33, 66, 100 })]
        [TestCase(10, new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 })]
        public void Run_SupplyFilesToCopy_BackgroundWorkerCallsOnProgressWithExpectedPercentForEachFile(int filesCount, int[] expectedPercents)
        {
            // arrange
            List<int> percentsToVerify = new List<int>();
            List<AppFile> filesToCopy = new List<AppFile>();
            Enumerable.Range(0, filesCount).ToList().ForEach(i => filesToCopy.Add(new AppFile()));

            settingsMock.SetupGet(x => x.FilesToCopy).Returns(filesToCopy);

            _backgroundWorkerMock
                .Setup(x => x.ReportProgress(It.IsAny<int>(), It.IsAny<object>()))
                .Callback<int, object>((percentProgress, argument) =>
                {
                    _backgroundWorkerMock.Raise(y => y.OnProgressChanged += null, null, new System.ComponentModel.ProgressChangedEventArgs(percentProgress, argument));
                });

            _parameters.OnProgressChanged = (index) => percentsToVerify.Add(index);

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            CollectionAssert.AreEqual(expectedPercents, percentsToVerify.ToArray());
        }

        [Test]
        public void Run_SupplyFiveFilesAndUniqueNameGeneratorAndCharsPositionOnCopy_UniqueNameGeneratorGetsCalledForEachFileWithExactSettings()
        {
            // arrange
            List<AppFile> filesToCopy = InitializeFiles();

            settingsMock.Setup(s => s.FilesToCopy).Returns(filesToCopy);
            settingsMock.SetupGet(s => s.CharsPositionOnCopy).Returns(UniqueCharsPosition.Prefix);

            int fileIndex = 0;

            fileHelperMock
                .Setup(s => s.GenerateUniqueFileName(It.IsAny<AppFile>(), generatorMock.Object, UniqueCharsPosition.Prefix))
                .Callback((AppFile file, IUniqueCharsGenerator generator, UniqueCharsPosition position) =>
                {
                    Assert.That(file, Is.EqualTo(filesToCopy[fileIndex]));
                    Assert.That(generator, Is.EqualTo(generatorMock.Object));
                    Assert.That(position, Is.EqualTo(UniqueCharsPosition.Prefix));
                    ++fileIndex;
                });

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            fileHelperMock.Verify();
        }

        [Test]
        public void Run_SupplyFiveFilesAndFirstFileAlreadyExistsAndItShouldNotBeCopied_FirstFileIsNotCopied()
        {
            // arrange
            List<AppFile> filesToCopy = InitializeFiles();

            string pathTo = "Z:/dummy_folder";
            string existingFilePath = string.Format("{0}/{1}", pathTo, filesToCopy[0].FileNameFull);
            string sourceFilePath = filesToCopy[0].FilePath;

            AppFile currentFile = new AppFile();
            fileHelperMock.Setup(x => x.GenerateUniqueFileName(It.IsAny<AppFile>(), It.IsAny<IUniqueCharsGenerator>(), It.IsAny<UniqueCharsPosition>()))
                .Callback<AppFile, IUniqueCharsGenerator, UniqueCharsPosition>((a, b, c) =>
                {
                    currentFile = a;
                })
                .Returns(() => currentFile.FileNameFull);
            fileSystemMock.Setup(s => s.FileExists(existingFilePath)).Returns(true);
            fileSystemMock.Setup(s => s.CopyTo(It.IsAny<string>(), It.IsAny<string>()));

            settingsMock.Setup(s => s.FilesToCopy).Returns(filesToCopy);
            settingsMock.SetupGet(s => s.OnDuplicateDoNotCopy).Returns(true);
            settingsMock.SetupGet(s => s.PathTo).Returns(pathTo);

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            fileSystemMock.Verify(x => x.CopyTo(sourceFilePath, existingFilePath), Times.Never());
            settingsMock.VerifyGet(x => x.OnDuplicateDoNotCopy, Times.Once());
        }

        [Test]
        public void Run_SupplyFiveFilesAndFirstFileAlreadyExistsAndItShouldBeOverwritten_FirstFileIsOverwritten()
        {
            // arrange
            List<AppFile> filesToCopy = InitializeFiles();

            string pathTo = "Z:/dummy_folder";
            string existingFilePath = string.Format("{0}/{1}", pathTo, filesToCopy[0].FileNameFull);
            string sourceFilePath = filesToCopy[0].FilePath;

            AppFile currentFile = new AppFile();
            fileHelperMock.Setup(x => x.GenerateUniqueFileName(It.IsAny<AppFile>(), It.IsAny<IUniqueCharsGenerator>(), It.IsAny<UniqueCharsPosition>()))
                .Callback<AppFile, IUniqueCharsGenerator, UniqueCharsPosition>((a, b, c) =>
                {
                    currentFile = a;
                })
                .Returns(() => currentFile.FileNameFull);

            fileSystemMock.Setup(s => s.FileExists(existingFilePath)).Returns(true);
            fileSystemMock.Setup(s => s.DeleteFile(existingFilePath));
            fileSystemMock.Setup(s => s.CopyTo(It.IsAny<string>(), It.IsAny<string>()));

            settingsMock.Setup(s => s.FilesToCopy).Returns(filesToCopy);
            settingsMock.SetupGet(s => s.OnDuplicateOverwrite).Returns(true);
            settingsMock.SetupGet(s => s.PathTo).Returns(pathTo);

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            fileSystemMock.Verify(x => x.DeleteFile(existingFilePath), Times.Once());
            fileSystemMock.Verify(x => x.CopyTo(sourceFilePath, existingFilePath), Times.Once());
            settingsMock.VerifyGet(x => x.OnDuplicateOverwrite, Times.Once());
        }

        [Test]
        public void Run_SupplyFiveFilesAndFirstFileAlreadyExistsAndItShouldNotAddUniqueCharsSequence_FirstFileDoesntHaveUniqueCharsSequence()
        {
            // arrange
            List<AppFile> filesToCopy = InitializeFiles();

            AppFile existingFile = filesToCopy[0];

            fileSystemMock.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);

            fileHelperMock.Setup(s => s.GenerateUniqueFileName(It.IsAny<AppFile>(), It.IsAny<IUniqueCharsGenerator>(), It.IsAny<UniqueCharsPosition>()));

            settingsMock.Setup(s => s.FilesToCopy).Returns(filesToCopy);

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            fileHelperMock.Verify(s => s.GenerateUniqueFileName(existingFile, generatorMock.Object, UniqueCharsPosition.Prefix), Times.Never());
        }

        [TestCase(UniqueCharsPosition.Prefix, true, false)]
        [TestCase(UniqueCharsPosition.Suffix, false, true)]
        public void Run_SupplyFiveFilesAndFirstFileAlreadyExistsAndItShouldHaveUniqueCharsSequenceAdded_FirstFileHasUniqueCharsSequence(UniqueCharsPosition uniqueCharsPosition, bool onDuplicateAddPrefix, bool onDuplicateAddPostfix)
        {
            // arrange
            List<AppFile> filesToCopy = InitializeFiles();

            AppFile existingFile = filesToCopy[0];

            fileSystemMock.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);

            fileHelperMock.Setup(s => s.GenerateUniqueFileName(It.IsAny<AppFile>(), It.IsAny<IUniqueCharsGenerator>(), It.IsAny<UniqueCharsPosition>()));

            settingsMock.Setup(s => s.FilesToCopy).Returns(filesToCopy);
            settingsMock.SetupGet(s => s.OnDuplicateAddPrefix).Returns(onDuplicateAddPrefix);
            settingsMock.SetupGet(s => s.OnDuplicateAddSuffix).Returns(onDuplicateAddPostfix);
            settingsMock.SetupGet(s => s.CharsPositionOnDuplicate).Returns(uniqueCharsPosition);

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            fileHelperMock.Verify(s => s.GenerateUniqueFileName(existingFile, generatorMock.Object, uniqueCharsPosition), Times.Once());
        }

        [Test]
        public void Run_BackgroundWorkerThrewError_OnFailedIsCalledBySyncContext()
        {
            // arrange
            Exception error = new Exception("test");
            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(null, error, false);

            Mock<Action<string>> onFailedMock = new Mock<Action<string>>();
            onFailedMock.Setup(x => x(error.Message));
            _parameters.OnFailed = onFailedMock.Object;

            _worker.Run(_parameters);

            // act
            _backgroundWorkerMock.Raise(x => { x.OnRunWorkerCompleted += null; }, null, eventArgs);

            // assert
            onFailedMock.Verify(x => x(error.Message), Times.Once);
        }

        [Test]
        public void Run_BackgroundWorkerCancelsProcess_OnCancelledIsCalledBySyncContext()
        {
            // arrange
            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(null, null, true);

            Mock<Action> onCancelledMock = new Mock<Action>();
            onCancelledMock.Setup(x => x());
            _parameters.OnCancelled = onCancelledMock.Object;

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnRunWorkerCompleted += null; }, null, eventArgs);

            // assert
            onCancelledMock.Verify(x => x(), Times.Once);
        }

        [Test]
        public void Run_BackgroundWorkerSuccessfullyFinishesProcess_OnFinishedIsCalledBySyncContext()
        {
            // arrange
            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(null, null, false);

            Mock<Action> onFinishedMock = new Mock<Action>();
            onFinishedMock.Setup(x => x());
            _parameters.OnFinished = onFinishedMock.Object;

            // act
            _worker.Run(_parameters);
            _backgroundWorkerMock.Raise(x => { x.OnRunWorkerCompleted += null; }, null, eventArgs);

            // assert
            onFinishedMock.Verify(x => x(), Times.Once);
        }

        [Test]
        public void Run_BackgroundWorkerIsBusy_CallsCancelAsync()
        {
            // arrange
            _backgroundWorkerMock.SetupGet(x => x.IsBusy).Returns(true);
            _backgroundWorkerMock.Setup(x => x.CancelAsync());

            // act
            _worker.Run(_parameters);

            // assert
            _backgroundWorkerMock.Verify(x => x.CancelAsync(), Times.Once);
        }

        [Test]
        public void Cancel_CallsBackgroundWorkerCallsCancelAsync()
        {
            // arrange
            _backgroundWorkerMock.Setup(x => x.CancelAsync());

            // act
            _worker.Cancel();

            // assert
            _backgroundWorkerMock.Verify(x => x.CancelAsync(), Times.Once);
        }

        [Test]
        public void Run_BackgroundWorker_CallsRunWorkerAsync()
        {
            // arrange
            _backgroundWorkerMock.Setup(x => x.RunWorkerAsync());

            // act
            _worker.Run(_parameters);

            // assert
            _backgroundWorkerMock.Verify(x => x.RunWorkerAsync(), Times.Once);
        }
    }
}