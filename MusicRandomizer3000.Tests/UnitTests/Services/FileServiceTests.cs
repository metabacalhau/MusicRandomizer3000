using MusicRandomizer3000.Core.Models;
using MusicRandomizer3000.Core.Services;
using MusicRandomizer3000.Core.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace MusicRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class FileServiceTests
    {
        CultureInfo savedCulture;

        Mock<IFileSystem> fileSystemMock = null;
        Mock<IFileHelper> fileHelperMock = null;
        Mock<IEqualityComparer<AppFile>> fileComparerMock = null;

        private void InitializeMocks()
        {
            fileSystemMock = new Mock<IFileSystem>();
            fileHelperMock = new Mock<IFileHelper>();
            fileComparerMock = new Mock<IEqualityComparer<AppFile>>();
        }

        private FileService InitializeConstructor()
        {
            return new FileService(fileSystemMock.Object, fileHelperMock.Object, fileComparerMock.Object);
        }

        private CultureInfo SetInvariantCultureInfo()
        {
            savedCulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            return Thread.CurrentThread.CurrentCulture;
        }

        private void RestoreCultureInfo()
        {
            Thread.CurrentThread.CurrentCulture = savedCulture;
        }

        [TestCase(null, null, null, "fileSystem")]
        [TestCase(true, null, null, "fileHelper")]
        [TestCase(true, true, null, "fileComparer")]
        public void FileService_SupplyInvalidArguments_ThrowsArgumentNullException(object fileSystem, object fileHelper, object fileComparer, string expectedErrorMessage)
        {
            // arrange
            InitializeMocks();

            TestDelegate testDelegate = () => new FileService
            (
                fileSystem != null ? fileSystemMock.Object : null,
                fileHelper != null ? fileHelperMock.Object : null,
                fileComparer != null ? fileComparerMock.Object : null
            );

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo(expectedErrorMessage));
        }

        [Test]
        public void GetFiles_SupplyFolderPath_ReturnsFilesInsideFolder()
        {
            // arrange
            InitializeMocks();

            string filePath = "some_file_path";

            IEnumerable<string> filePaths = new List<string> { "file1_path", "file2_path", "file3_path" };

            IEnumerable<AppFile> expectedResult = filePaths.Select(x => new AppFile { FilePath = x });

            fileSystemMock.Setup(x => x.GetFiles(filePath)).Returns(filePaths);

            fileSystemMock.Setup(x => x.CreateAppFile(It.IsAny<string>())).Returns<string>((path) => expectedResult.Where(x => x.FilePath == path).Single());

            FileService fileService = InitializeConstructor();

            // act
            IEnumerable<AppFile> result = fileService.GetFiles(filePath);

            // assert
            Assert.That(expectedResult, Has.All.Matches<AppFile>(x => result.Any(z => z.FilePath.Equals(x.FilePath))));
        }

        [Test]
        public void GetFilesByMaxFileSize_NonUniqueFiles_SupplyNullFilesCollection_ThrowsFilesArgumentNullException()
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            TestDelegate testDelegate = () => fileService.GetFilesByMaxFileSize(false, null, null, 0).ToList();

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("files"));
        }

        [TestCase(0)]
        [TestCase(-0.9)]
        [TestCase(-10)]
        public void GetFilesByMaxFileSize_NonUniqueFiles_SupplyZeroOrLessFileSizeLimit_ThrowsFileSizeException(double maxSizeLimit)
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            TestDelegate testDelegate = () => fileService.GetFilesByMaxFileSize(false, new List<AppFile>(), null, maxSizeLimit).ToList();

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("maxSizeLimit"));
        }

        [TestCase(new double[] { }, 0.9, 0)]
        [TestCase(new double[] { 1 }, 0.9, 0)]
        [TestCase(new double[] { 0.999 }, 1, 1)]
        [TestCase(new double[] { 1 }, 3, 2)]
        [TestCase(new double[] { 5.5, 4.5 }, 11, 2)]
        [TestCase(new double[] { 2 }, 11, 5)]
        [TestCase(new double[] { 20.2, 20.3, 4 }, 45.5, 3)]
        [TestCase(new double[] { 10.9, 10, 10, 10, 59 }, 51, 4)]
        public void GetFilesByMaxFileSize_NonUniqueFiles_SupplyFilesAndMaxSizeLimit_ReturnsNonUniqueRandomFilesLessThanMaxSizeLimit(double[] fileSizes, double sizeLimit, int expectedFilesCount)
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            List<AppFile> files = null;

            if (fileSizes != null)
            {
                files = new List<AppFile>();

                foreach (double fileSize in fileSizes)
                {
                    files.Add(new AppFile { FileSize = fileSize });
                }
            }

            List<AppFile> returnedFiled = new List<AppFile>();

            fileHelperMock.Setup(x => x.GetRandomFile(files)).Returns<IEnumerable<AppFile>>((collection) =>
            {
                AppFile fileToReturn = null;

                fileToReturn = collection.Where(x => !returnedFiled.Contains(x)).FirstOrDefault();

                if (fileToReturn == null)
                {
                    fileToReturn = collection.First();
                }

                returnedFiled.Add(fileToReturn);

                return fileToReturn;
            });

            // act
            IEnumerable<AppFile> result = fileService.GetFilesByMaxFileSize(false, files, null, sizeLimit);

            // assert
            Assert.AreEqual(expectedFilesCount, result.ToList().Count);
        }

        [TestCase(new double[] { }, new double[] { }, 0.9, 0)]
        [TestCase(new double[] { 1 }, new double[] { 1, 0.5 }, 0.9, 0)]
        [TestCase(new double[] { 0.5, 0.5, 0.6 }, new double[] { 0.5 }, 1.3, 2)]
        [TestCase(new double[] { 0.999 }, new double[] { 0.999 }, 1, 1)]
        [TestCase(new double[] { 1 }, new double[] { 5 }, 5, 4)]
        [TestCase(new double[] { 1, 2, 3 }, new double[] { 1 }, 6, 2)]
        [TestCase(new double[] { 5.5, 4.5 }, new double[] { 1, 2, 3, 4, 5 }, 11, 2)]
        public void GetFilesByMaxFileSize_NonUniqueFiles_SupplyFilesAndExistingFilesAndSizeLimit_ReturnsNonUniqueAndNonExistingRandomFilesLessOrEqualToMaximumSizeLimit(double[] fileSizes, double[] existingFileSizes, double sizeLimit, int expectedFilesCount)
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            List<AppFile> files = null;

            if (fileSizes != null)
            {
                files = new List<AppFile>();

                foreach (double fileSize in fileSizes)
                {
                    files.Add(new AppFile { FileNameFull = "file", FileSize = fileSize });
                }
            }

            List<AppFile> existingFiles = null;

            if (existingFileSizes != null)
            {
                existingFiles = new List<AppFile>();

                foreach (double fileSize in existingFileSizes)
                {
                    existingFiles.Add(new AppFile { FileNameFull = "file", FileSize = fileSize });
                }
            }

            List<AppFile> returnedFiles = new List<AppFile>();

            fileHelperMock.Setup(x => x.GetRandomFile(It.IsAny<IEnumerable<AppFile>>())).Returns<IEnumerable<AppFile>>((collection) =>
            {
                AppFile fileToReturn = null;

                fileToReturn = collection.Where(x => !returnedFiles.Contains(x)).FirstOrDefault();

                if (fileToReturn == null)
                {
                    fileToReturn = collection.First();
                }

                returnedFiles.Add(fileToReturn);

                return fileToReturn;
            });

            // act
            IEnumerable<AppFile> result = fileService.GetFilesByMaxFileSize(false, files, existingFiles, sizeLimit);

            // assert
            Assert.AreEqual(expectedFilesCount, result.ToList().Count);
        }

        [Test]
        public void GetFilesByMaxFileSize_UniqueFiles_SupplyNullFilesCollection_ThrowsFilesArgumentNullException()
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            TestDelegate testDelegate = () => fileService.GetFilesByMaxFileSize(true, null, null, 0).ToList();

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("files"));
        }

        [TestCase(0)]
        [TestCase(-0.9)]
        [TestCase(-10)]
        public void GetFilesByMaxFileSize_UniqueFiles_SupplyZeroOrLessFileSizeLimit_ThrowsFileSizeException(double maxSizeLimit)
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            TestDelegate testDelegate = () => fileService.GetFilesByMaxFileSize(true, new List<AppFile>(), null, maxSizeLimit).ToList();

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("maxSizeLimit"));
        }

        [TestCase(new double[] { }, 5, 0)]
        [TestCase(new double[] { 1, 1 }, 5, 2)]
        [TestCase(new double[] { 1, 2 }, 5, 2)]
        [TestCase(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.6, 0.6 }, 5, 8)]
        [TestCase(new double[] { 10.1, 10.2, 10.3, 10.4, 10.5, 10.6, 0.5 }, 62.7, 7)]
        public void GetFilesByMaxFileSize_UniqueFiles_SupplyFilesAndSizeLimit_ReturnsUniqueRandomFilesLessOrEqualToMaximumSizeLimit(double[] fileSizes, double maxSizeLimit, int expectedFilesCount)
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            List<AppFile> files = null;

            if (fileSizes != null)
            {
                files = new List<AppFile>();

                foreach (double fileSize in fileSizes)
                {
                    files.Add(new AppFile { FileSize = fileSize });
                }
            }

            List<AppFile> returnedFiled = new List<AppFile>();

            fileHelperMock.Setup(x => x.GetRandomFile(It.IsAny<IEnumerable<AppFile>>())).Returns<IEnumerable<AppFile>>((collection) =>
            {
                AppFile fileToReturn = null;

                fileToReturn = collection.Where(x => !returnedFiled.Contains(x)).FirstOrDefault();

                if (fileToReturn != null)
                {
                    returnedFiled.Add(fileToReturn);
                }

                return fileToReturn;
            });

            // act
            IEnumerable<AppFile> result = fileService.GetFilesByMaxFileSize(true, files, null, maxSizeLimit);

            // assert
            Assert.AreEqual(expectedFilesCount, result.ToList().Count);
        }

        [TestCase(new double[] { }, new double[] { }, 0.9, 0)]
        [TestCase(new double[] { 1 }, new double[] { 1, 0.5 }, 0.9, 0)]
        [TestCase(new double[] { 0.5, 0.5, 0.6 }, new double[] { 0.5 }, 1.3, 2)]
        [TestCase(new double[] { 0.999 }, new double[] { 0.999 }, 1, 1)]
        [TestCase(new double[] { 1 }, new double[] { 5 }, 5, 1)]
        [TestCase(new double[] { 1, 2, 3 }, new double[] { 1 }, 6, 2)]
        [TestCase(new double[] { 5.5, 4.5 }, new double[] { 1, 2, 3, 4, 5 }, 11, 2)]
        public void GetFilesByMaxFileSize_UniqueFiles_SupplyFilesAndExistingFilesAndSizeLimit_ReturnsExpectedFilesLessOrEqualToMaximumSizeLimit(double[] fileSizes, double[] existingFileSizes, double sizeLimit, int expectedFilesCount)
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            List<AppFile> files = null;

            if (fileSizes != null)
            {
                files = new List<AppFile>();

                foreach (double fileSize in fileSizes)
                {
                    files.Add(new AppFile { FileNameFull = "file", FileSize = fileSize });
                }
            }

            List<AppFile> existingFiles = null;

            if (existingFileSizes != null)
            {
                existingFiles = new List<AppFile>();

                foreach (double fileSize in existingFileSizes)
                {
                    existingFiles.Add(new AppFile { FileNameFull = "file", FileSize = fileSize });
                }
            }

            List<AppFile> returnedFiles = new List<AppFile>();

            fileHelperMock.Setup(x => x.GetRandomFile(It.IsAny<IEnumerable<AppFile>>())).Returns<IEnumerable<AppFile>>((collection) =>
            {
                AppFile fileToReturn = null;

                fileToReturn = collection.Where(x => !returnedFiles.Contains(x)).FirstOrDefault();

                if (fileToReturn != null)
                {
                    returnedFiles.Add(fileToReturn);
                }

                return fileToReturn;
            });

            // act
            IEnumerable<AppFile> result = fileService.GetFilesByMaxFileSize(true, files, existingFiles, sizeLimit);

            // assert
            Assert.AreEqual(expectedFilesCount, result.ToList().Count);
        }

        [Test]
        [Category("DeleteFiles")]
        public void DeleteFiles_SupplyNoFiles_DeleteFileFunctionIsNotCalled()
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            // act
            fileService.DeleteFiles(null);

            // assert
            fileSystemMock.Verify(x => x.DeleteFile(It.IsAny<string>()), Times.Never());
        }

        [Test]
        [Category("DeleteFiles")]
        public void DeleteFiles_SupplyFiles_DeleteFileFunctionIsCalledForEachFile()
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            List<AppFile> files = new List<AppFile>
            {
                new AppFile { FilePath = "file1_path" },
                new AppFile { FilePath = "file2_path" },
                new AppFile { FilePath = "file3_path" }
            };

            int fileIndex = 0;

            fileSystemMock
                .Setup(x => x.DeleteFile(It.IsAny<string>()))
                .Callback((string filePath) =>
                {
                    Assert.That(filePath, Is.EqualTo(files[fileIndex].FilePath));

                    ++fileIndex;
                });

            // act
            fileService.DeleteFiles(files);

            // assert
            fileSystemMock.Verify(x => x.DeleteFile(It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        [Category("GetTotalSize")]
        public void GetTotalSize_SupplyNoFiles_ReturnsZeroValue()
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            // act
            double result = fileService.GetTotalSize(null);

            // assert
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("GetTotalSizeFormatted")]
        public void GetSizeFormatted_SupplyNoFiles_ReturnsZeroValueFormatted()
        {
            // arrange
            InitializeMocks();

            FileService fileService = InitializeConstructor();

            fileHelperMock.Setup(x => x.FileSizeFormatter(0)).Returns("0");

            // act
            string result = fileService.GetTotalSizeFormatted(null);

            // assert
            Assert.AreEqual("0", result);
        }

        [TestCase(new double[] { 0, 0.09 }, "0.09")]
        [TestCase(new double[] { 0, 1 }, "1")]
        [TestCase(new double[] { 0, 10000, 999999.99999 }, "1009999.99999")]
        [Category("GetTotalSizeFormatted")]
        public void GetSizeFormatted_SupplyNoFiles_ReturnsZeroValueFormatted(double[] fileSizes, string expectedFormattedSize)
        {
            // arrange
            SetInvariantCultureInfo();

            InitializeMocks();

            FileService fileService = InitializeConstructor();

            List<AppFile> files = new List<AppFile>();

            foreach (double fileSize in fileSizes)
            {
                files.Add(new AppFile { FileSize = fileSize });
            }

            string mockResult = "";

            fileHelperMock
                .Setup(x => x.FileSizeFormatter(It.IsAny<double>()))
                .Callback((double totalFileSize) =>
                {
                    mockResult = totalFileSize.ToString();
                });

            // act
            fileService.GetTotalSizeFormatted(files);

            // assert
            Assert.AreEqual(expectedFormattedSize, mockResult);

            RestoreCultureInfo();
        }
    }
}