using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services;
using FileRandomizer3000.Core.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileRandomizer3000.Tests.UnitTests.Services
{
    public class TraverseServiceTests
    {
        Mock<IFileService> fileServiceMock = null;
        Mock<IFolderService> folderServiceMock = null;

        private void InitializeMocks()
        {
            fileServiceMock = new Mock<IFileService>();
            folderServiceMock = new Mock<IFolderService>();
        }

        private TraverseService InitializeConstructor()
        {
            return new TraverseService(fileServiceMock.Object, folderServiceMock.Object);
        }

        [TestCase(null, null, "fileService")]
        [TestCase(true, null, "folderService")]
        public void TraverseService_SupplyNullArguments_ThrowsArgumentNullException(object fileService, object folderService, string expectedExceptionMessage)
        {
            // arrange
            InitializeMocks();

            IFileService fileServiceArgument = fileService != null ? fileServiceMock.Object : (IFileService)fileService;
            IFolderService folderServiceArgument = folderService != null ? folderServiceMock.Object : (IFolderService)folderService;

            TestDelegate testDelegate = () => new TraverseService(fileServiceArgument, folderServiceArgument);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void TraverseFolder_SupplyInaccessibleFolder_ReturnsNoFiles()
        {
            // arrange
            InitializeMocks();

            string folderPath = "path";

            folderServiceMock.Setup(x => x.IsAccessible(folderPath)).Returns(false);

            TraverseService service = InitializeConstructor();

            // act
            IEnumerable<AppFile> result = service.TraverseFolder(folderPath);

            // assert
            Assert.AreEqual(0, result.ToList().Count);
        }

        [Test]
        public void TraverseFolder_SupplyFolderWithFilesInside_ReturnsFilesInSuppliedFolder()
        {
            // arrange
            InitializeMocks();

            string folderPath = "path";

            List<AppFile> files = new List<AppFile>
            {
                new AppFile {},
                new AppFile {},
                new AppFile {}
            };

            folderServiceMock.Setup(x => x.IsAccessible(folderPath)).Returns(true);

            fileServiceMock.Setup(x => x.GetFiles(folderPath)).Returns(files);

            TraverseService service = InitializeConstructor();

            // act
            IEnumerable<AppFile> result = service.TraverseFolder(folderPath);

            // assert
            Assert.AreEqual(files.Count, result.ToList().Count);
        }

        [Test]
        public void TraverseFolder_SupplyFolderWithFilesAndFoldersWithFiles_ReturnsFilesInSuppliedFolder()
        {
            // arrange
            InitializeMocks();

            string folderPath = "path";

            List<AppFile> files = new List<AppFile>
            {
                new AppFile {},
                new AppFile {},
                new AppFile {}
            };

            folderServiceMock.Setup(x => x.IsAccessible(folderPath)).Returns(true);

            fileServiceMock.Setup(x => x.GetFiles(folderPath)).Returns(files);

            TraverseService service = InitializeConstructor();

            // act
            IEnumerable<AppFile> result = service.TraverseFolder(folderPath);

            // assert
            Assert.AreEqual(files.Count, result.ToList().Count);
        }

        [Test]
        public void TraverseFolder_GetFilesFromAccessibleFoldersInsideSuppliedFolder_ReturnsFilesOnlyFromAccessibleFolders()
        {
            // arrange
            InitializeMocks();

            TestFoldersAndFiles testData = new TestFoldersAndFiles();

            List<AppFolder> folders = new List<AppFolder>();

            TestFolder rootFolder = testData.FoldersAndFiles.First();

            foreach (TestFolder testFolder in testData.FoldersAndFiles)
            {
                SetUpFolder(testFolder);
            }

            TraverseService service = InitializeConstructor();

            // act
            IEnumerable<AppFile> result = service.TraverseFolder(rootFolder.FolderPath);

            // assert
            Assert.AreEqual(10, result.ToList().Count);
        }

        private void SetUpFolder(TestFolder testFolder)
        {
            folderServiceMock.Setup(x => x.IsAccessible(testFolder.FolderPath)).Returns(testFolder.IsAccessible);

            if (testFolder.Files != null && testFolder.Files.Any())
            {
                List<AppFile> files = new List<AppFile>();

                foreach (AppFile file in testFolder.Files)
                {
                    files.Add(file);
                }

                fileServiceMock.Setup(x => x.GetFiles(testFolder.FolderPath)).Returns(files);
            }

            if (testFolder.IsAccessible)
            {
                if (testFolder.Folders != null && testFolder.Folders.Any())
                {
                    List<AppFolder> folders = new List<AppFolder>();

                    foreach (TestFolder folder in testFolder.Folders)
                    {
                        folders.Add(new AppFolder { FolderPath = folder.FolderPath });

                        SetUpFolder(folder);
                    }

                    folderServiceMock.Setup(x => x.GetFolders(testFolder.FolderPath)).Returns(folders);
                }
            }
        }

        private class TestFoldersAndFiles
        {
            public List<TestFolder> FoldersAndFiles
            {
                get
                {
                    List<TestFolder> testData = new List<TestFolder>
                    {
                        new TestFolder
                        {
                            FolderPath = "path0",
                            IsAccessible = true,
                            Folders = new List<TestFolder>
                            {
                                new TestFolder
                                {
                                    FolderPath = "path1",
                                    IsAccessible = true,
                                    Files = new List<AppFile>
                                    {
                                        new AppFile { FileNameWithoutExtension = "path1 - file1" }
                                    },
                                    Folders = new List<TestFolder>
                                    {
                                        new TestFolder
                                        {
                                            FolderPath = "path1.1",
                                            IsAccessible = true,
                                            Files = new List<AppFile>
                                            {
                                                new AppFile { FileNameWithoutExtension = "path1.1 - file1" },
                                                new AppFile { FileNameWithoutExtension = "path1.1 - file2" }
                                            }
                                        },
                                        new TestFolder
                                        {
                                            FolderPath = "path1.2",
                                            IsAccessible = true,
                                            Files = new List<AppFile>
                                            {
                                                new AppFile { FileNameWithoutExtension = "path1.2 - file1" },
                                                new AppFile { FileNameWithoutExtension = "path1.2 - file2" }
                                            },
                                            Folders = new List<TestFolder>
                                            {
                                                new TestFolder
                                                {
                                                    FolderPath = "path1.2.1",
                                                    IsAccessible = false,
                                                    Files = new List<AppFile>
                                                    {
                                                        new AppFile { FileNameWithoutExtension = "path1.2.1 - file1" },
                                                        new AppFile { FileNameWithoutExtension = "path1.2.1 - file2" }
                                                    }
                                                },
                                                new TestFolder
                                                {
                                                    FolderPath = "path1.2.2",
                                                    IsAccessible = true,
                                                    Files = new List<AppFile>
                                                    {
                                                        new AppFile { FileNameWithoutExtension = "path1.2.2 - file1" },
                                                        new AppFile { FileNameWithoutExtension = "path1.2.2 - file2" }
                                                    }
                                                }
                                            }
                                        },
                                        new TestFolder
                                        {
                                            FolderPath = "path1.3",
                                            IsAccessible = false,
                                            Files = new List<AppFile>
                                            {
                                                new AppFile { FileNameWithoutExtension = "path1.3 - file1" },
                                                new AppFile { FileNameWithoutExtension = "path1.3 - file2" },
                                                new AppFile { FileNameWithoutExtension = "path1.3 - file3" },
                                            }
                                        }
                                    }
                                },
                                new TestFolder
                                {
                                    FolderPath = "path2",
                                    IsAccessible = true,
                                    Files = new List<AppFile>
                                    {
                                        new AppFile { FileNameWithoutExtension = "path2 - file1" }
                                    }
                                },
                                new TestFolder
                                {
                                    FolderPath = "path3",
                                    IsAccessible = true,
                                    Files = new List<AppFile>
                                    {
                                        new AppFile { FileNameWithoutExtension = "path3 - file1" }
                                    }
                                },
                                new TestFolder
                                {
                                    FolderPath = "path4",
                                    IsAccessible = false,
                                    Files = new List<AppFile>
                                    {
                                        new AppFile { FileNameWithoutExtension = "path4 - file1" }
                                    },
                                    Folders = new List<TestFolder>
                                    {
                                        new TestFolder
                                        {
                                            FolderPath = "path4.1",
                                            IsAccessible = true,
                                            Files = new List<AppFile>
                                            {
                                                new AppFile { FileNameWithoutExtension = "path4.1 - file1" }
                                            }
                                        }
                                    }
                                },
                                new TestFolder
                                {
                                    FolderPath = "path5",
                                    IsAccessible = true,
                                    Files = new List<AppFile>
                                    {
                                        new AppFile { FileNameWithoutExtension = "path5 - file1" }
                                    }
                                }
                            }
                        }
                    };

                    return testData;
                }
            }
        }

        private class TestFolder
        {
            public string FolderPath { get; set; }
            public bool IsAccessible { get; set; }
            public List<AppFile> Files { get; set; }
            public List<TestFolder> Folders { get; set; }
        }
    }
}