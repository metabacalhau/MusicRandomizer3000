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
    public class RandomizerWorkerTests
    {
        Mock<IFileService> _fileServiceMock = null;
        Mock<ITraverseService> _traverseServiceMock = null;
        ContextStub _uiContextStub = null;
        Mock<IBackgroundWorker> _backgroundWorkerMock = null;
        RandomizerWorkerSettings _settings = null;
        RandomizerWorker _worker = null;

        [SetUp]
        public void SetUp()
        {
            _fileServiceMock = new Mock<IFileService>();
            _traverseServiceMock = new Mock<ITraverseService>();
            _uiContextStub = new ContextStub();
            _backgroundWorkerMock = new Mock<IBackgroundWorker>();
            _worker = new RandomizerWorker(_fileServiceMock.Object, _traverseServiceMock.Object, _uiContextStub, _backgroundWorkerMock.Object);
        }

        private class FileEnumerationStub
        {
            private Mock<IBackgroundWorker> _backgroundWorker;

            public FileEnumerationStub(Mock<IBackgroundWorker> backgroundWorker)
            {
                _backgroundWorker = backgroundWorker;
            }

            public int filesCalledIndex = 0;

            public IEnumerable<AppFile> GetFiles()
            {
                for (int i = 0; i < 5; i++)
                {
                    ++filesCalledIndex;

                    yield return new AppFile();
                }
            }

            public IEnumerable<AppFile> GetFilesWithCancelCallback()
            {
                for (int i = 0; i < 5; i++)
                {
                    ++filesCalledIndex;

                    if (filesCalledIndex == 3)
                    {
                        _backgroundWorker.SetupGet(x => x.CancellationPending).Returns(true);
                    }

                    yield return new AppFile();
                }
            }
        }

        [Test]
        public void RandomizerWorker_SupplyNullFileServiceArgument_ThrowsFileServiceArgumentNullException()
        {
            TestDelegate testDelegate = () => new RandomizerWorker(null, null, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("fileService"));
        }

        [Test]
        public void RandomizerWorker_SupplyNullFolderServiceArgument_ThrowsFolderServiceArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new RandomizerWorker(_fileServiceMock.Object, null, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("traverseService"));
        }

        [Test]
        public void RandomizerWorker_SupplyNullContextArgument_ThrowsContextArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new RandomizerWorker(_fileServiceMock.Object, _traverseServiceMock.Object, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("uiContext"));
        }

        [Test]
        public void RandomizerWorker_SupplyNullBackgroundWorkerArgument_ThrowsBackgroundWorkerArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => new RandomizerWorker(_fileServiceMock.Object, _traverseServiceMock.Object, _uiContextStub, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("backgroundWorker"));
        }

        [Test]
        public void Run_SupplyNullSettings_ThrowsSettingsArgumentNullException()
        {
            // arrange
            TestDelegate testDelegate = () => _worker.Run(null, null, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("settings"));
        }

        [Test]
        public void Run_SupplyNullOnFinishedCallback_ThrowsOnFinishedArgumentNullException()
        {
            // arrange
            _settings = new RandomizerWorkerSettings();

            TestDelegate testDelegate = () => _worker.Run(_settings, null, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onFinished"));
        }

        [Test]
        public void Run_SupplyNullOnFailedCallbackArgument_ThrowsOnFailedArgumentNullException()
        {
            // arrange
            _settings = new RandomizerWorkerSettings();

            TestDelegate testDelegate = () => _worker.Run(new RandomizerWorkerSettings(), (files) => { }, null, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onFailed"));
        }

        [Test]
        public void Run_SupplyNullOnCancelledCallbackArgument_ThrowsOnFailedArgumentNullException()
        {
            // arrange
            _settings = new RandomizerWorkerSettings();

            TestDelegate testDelegate = () => _worker.Run(new RandomizerWorkerSettings(), (files) => { }, (str) => { }, null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("onCancelled"));
        }

        [Test]
        public void Run_CancellationIsPending_WorkerStopsAndCallsOnFinishedCallback()
        {
            // arrange
            _backgroundWorkerMock.SetupGet(x => x.CancellationPending).Returns(true);

            _settings = new RandomizerWorkerSettings();

            Action<List<AppFile>> onFinished = (files) => Assert.That(files.Count, Is.EqualTo(0));

            // act, assert
            _worker.Run(_settings, onFinished, (str) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));
        }

        [Test]
        public void Run_SupplySettingToDeleteFromTargetFolder_AllFilesAreDeleted()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                DeleteFromTargetFolder = true,
                PathTo = "path to"
            };

            List<AppFile> files = new List<AppFile>
            {
                new AppFile { },
                new AppFile { },
                new AppFile { }
            };

            _fileServiceMock.Setup(x => x.GetFiles(_settings.PathTo)).Returns(files);

            _fileServiceMock
                .Setup(x => x.DeleteFile(It.IsAny<AppFile>()))
                .Callback((AppFile file) =>
                {
                    Assert.That(file, Is.EqualTo(files.SingleOrDefault(x => x == file)));
                });

            // act, assert
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));
        }

        [Test]
        public void Run_SupplyFilesToDeleteFromTargetFolderAndCancellationPending_WorkerStopsAfterThirdFileAndDoesntContinueDeletingFiles()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                DeleteFromTargetFolder = true,
                PathTo = "path to"
            };

            List<AppFile> files = new List<AppFile>
            {
                new AppFile { },
                new AppFile { },
                new AppFile { },
                new AppFile { },
                new AppFile { },
                new AppFile { }
            };

            _fileServiceMock.Setup(x => x.GetFiles(_settings.PathTo)).Returns(files);

            int fileIndex = 0;

            _fileServiceMock
                .Setup(x => x.DeleteFile(It.IsAny<AppFile>()))
                .Callback((AppFile file) =>
                {
                    ++fileIndex;

                    if (fileIndex == 3)
                    {
                        _backgroundWorkerMock.SetupGet(x => x.CancellationPending).Returns(true);
                    }
                });

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            _fileServiceMock.Verify(x => x.DeleteFile(It.IsAny<AppFile>()), Times.Exactly(3));
        }

        [Test]
        public void Run_SupplySettingOnDuplicateDontCopy_ReturnsAllExistingFilesInTargetFolder()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                OnDuplicateDoNotCopy = true,
                PathTo = "path to"
            };

            List<AppFile> files = new List<AppFile>
            {
                new AppFile { },
                new AppFile { },
                new AppFile { }
            };

            _fileServiceMock.Setup(x => x.GetFiles(_settings.PathTo)).Returns(files);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            _fileServiceMock.Verify(x => x.GetFiles(_settings.PathTo), Times.Once());
        }

        [Test]
        public void Run_SupplySettingOnDuplicateDontCopyAndCancellationPending_WorkerStopsAfterThirdFileAndDoesntContinueIteratingFiles()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                OnDuplicateDoNotCopy = true,
                PathTo = "path to"
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            _fileServiceMock
                .Setup(x => x.GetFiles(_settings.PathTo))
                .Returns(stub.GetFilesWithCancelCallback);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(3));
        }

        [Test]
        public void Run_SupplySettingsUseRecursiveSearch_ExecutesTraverseFunctionAndReturnsFilesFromSourceFolder()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                UseRecursiveSearch = true,
                PathFrom = "path from"
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            _traverseServiceMock.Setup(x => x.TraverseFolder(_settings.PathFrom)).Returns(stub.GetFiles);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(5));
        }

        [Test]
        public void Run_SupplySettingsUseRecursiveSearchAndCancellationPending_WorkerStopsAfterThirdFileAndDoesntContinueTraverseFiles()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                UseRecursiveSearch = true,
                PathFrom = "path from"
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            _traverseServiceMock.Setup(x => x.TraverseFolder(_settings.PathFrom)).Returns(stub.GetFilesWithCancelCallback);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(3));
        }

        [Test]
        public void Run_SupplySettingNonRecursiveSearch_ReturnsFilesFromSourceFolder()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                UseRecursiveSearch = false,
                PathFrom = "path from"
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            _fileServiceMock.Setup(x => x.GetFiles(_settings.PathFrom)).Returns(stub.GetFiles);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(5));
        }

        [Test]
        public void Run_SupplySettingNonRecursiveSearch_WorkerStopsAfterThirdFileAndDoesntContinueIteratingFiles()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                UseRecursiveSearch = false,
                PathFrom = "path from"
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            _fileServiceMock.Setup(x => x.GetFiles(_settings.PathFrom)).Returns(stub.GetFilesWithCancelCallback);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(3));
        }

        [Test]
        public void Run_SettingsContainsFilesNumberLimit_ReturnsRandomNonUniqueFiles()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                FilesNumberLimit = 1
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            _fileServiceMock.Setup(x => x.GetFilesByFilesNumber(It.IsAny<bool>(), It.IsAny<IEnumerable<AppFile>>(), It.IsAny<IEnumerable<AppFile>>(), _settings.FilesNumberLimit)).Returns(stub.GetFiles);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(5));
        }

        [Test]
        public void Run_SettingsContainsFilesNumberLimit_WorkerStopsAfterThirdFileAndDoesntContinueIteratingFiles()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                FilesNumberLimit = 1
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            _fileServiceMock.Setup(x => x.GetFilesByFilesNumber(It.IsAny<bool>(), It.IsAny<IEnumerable<AppFile>>(), It.IsAny<IEnumerable<AppFile>>(), _settings.FilesNumberLimit)).Returns(stub.GetFilesWithCancelCallback);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(3));
        }

        [Test]
        public void Run_SettingsContainsFoldersNumberLimitAndFilesInFolderNumberLimit_ReturnsRandomNonUniqueFiles()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                FoldersNumberLimit = 1,
                FilesNumberPerFolderLimit = 1
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            //_fileServiceMock.Setup(x => x.GetFilesByFoldersAndFilesNumber(It.IsAny<bool>(), It.IsAny<IEnumerable<AppFile>>(), It.IsAny<IEnumerable<AppFile>>(), _settings.FoldersNumberLimit.Value, _settings.FilesInFolderNumberLimit.Value)).Returns(stub.GetFiles);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });

            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(5));
        }

        [Test]
        public void Run_SettingsContainsFoldersNumberLimitAndFilesInFolderNumberLimit_WorkerStopsAfterThirdFileAndDoesntContinueIteratingFiles()
        {
            // arrange
            _settings = new RandomizerWorkerSettings
            {
                FoldersNumberLimit = 1,
                FilesNumberPerFolderLimit = 1
            };

            FileEnumerationStub stub = new FileEnumerationStub(_backgroundWorkerMock);

            //_fileServiceMock.Setup(x => x.GetFilesByFoldersAndFilesNumber(It.IsAny<bool>(), It.IsAny<IEnumerable<AppFile>>(), It.IsAny<IEnumerable<AppFile>>(), _settings.FoldersNumberLimit.Value, _settings.FilesInFolderNumberLimit.Value)).Returns(stub.GetFilesWithCancelCallback);

            // act
            _worker.Run(_settings, (x) => { }, (y) => { }, () => { });
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, new System.ComponentModel.DoWorkEventArgs(null));

            // assert
            Assert.That(stub.filesCalledIndex, Is.EqualTo(3));
        }

        [Test]
        public void Run_BackgroundWorkerThrewError_OnFailedIsCalledBySyncContext()
        {
            // arrange
            Exception error = new Exception("test");
            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(null, error, false);

            Mock<Action<string>> onFailedMock = new Mock<Action<string>>();
            onFailedMock.Setup(x => x(error.Message));

            _worker.Run(new RandomizerWorkerSettings(), (x) => { }, onFailedMock.Object, () => { });

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

            // act
            _worker.Run(new RandomizerWorkerSettings(), (x) => { }, (y) => { }, onCancelledMock.Object);
            _backgroundWorkerMock.Raise(x => { x.OnRunWorkerCompleted += null; }, null, eventArgs);

            // assert
            onCancelledMock.Verify(x => x(), Times.Once);
        }

        [Test]
        public void Run_BackgroundWorkerSuccessfullyFinishesProcess_OnFinishedIsCalledBySyncContext()
        {
            // arrange
            object result = new List<AppFile>();
            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, null, false);

            Mock<Action<List<AppFile>>> onFinishedMock = new Mock<Action<List<AppFile>>>();
            onFinishedMock.Setup(x => x((List<AppFile>)result));

            // act
            _worker.Run(new RandomizerWorkerSettings(), onFinishedMock.Object, (y) => { }, () => { });
            _backgroundWorkerMock.Raise(x => { x.OnRunWorkerCompleted += null; }, null, eventArgs);

            // assert
            onFinishedMock.Verify(x => x((List<AppFile>)result), Times.Once);
        }

        [Test]
        public void Run_BackgroundWorkerOnDoWork_SetsFilesToEventResult()
        {
            // arrange
            var eventArgs = new System.ComponentModel.DoWorkEventArgs(null);

            // act
            _worker.Run(new RandomizerWorkerSettings(), (x) => { }, (y) => { }, () => { });
            _backgroundWorkerMock.Raise(x => { x.OnDoWork += null; }, null, eventArgs);

            // assert
            Assert.IsTrue(eventArgs.Result.GetType() == new List<AppFile>().GetType());
        }

        [Test]
        public void Run_BackgroundWorkerIsBusy_CallsCancelAsync()
        {
            // arrange
            _backgroundWorkerMock.SetupGet(x => x.IsBusy).Returns(true);
            _backgroundWorkerMock.Setup(x => x.CancelAsync());

            // act
            _worker.Run(new RandomizerWorkerSettings(), (x) => { }, (y) => { }, () => { });

            // assert
            _backgroundWorkerMock.Verify(x => x.CancelAsync(), Times.Once);
        }

        [Test]
        public void Run_BackgroundWorker_CallsRunWorkerAsync()
        {
            // arrange
            _backgroundWorkerMock.Setup(x => x.RunWorkerAsync());

            // act
            _worker.Run(new RandomizerWorkerSettings(), (x) => { }, (y) => { }, () => { });

            // assert
            _backgroundWorkerMock.Verify(x => x.RunWorkerAsync(), Times.Once);
        }

        [Test]
        public void Run_BackgroundWorkerMustSupportCancellation()
        {
            // arrange
            _backgroundWorkerMock.SetupSet(x => x.WorkerSupportsCancellation).Callback(x => Assert.IsTrue(x));

            // act
            _worker.Run(new RandomizerWorkerSettings(), (x) => { }, (y) => { }, () => { });
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
    }
}