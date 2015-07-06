using FileRandomizer3000.Core;
using FileRandomizer3000.Core.Utilities;
using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using FileRandomizer3000.Core.Enums;

namespace FileRandomizer3000.Tests.UnitTests.Utilities
{
    [TestFixture]
    public class FileHelperTests
    {
        CultureInfo savedCulture;

        private void SetInvariantCulture()
        {
            savedCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private void RestoreCulture()
        {
            Thread.CurrentThread.CurrentCulture = savedCulture;
        }

        [Test]
        [Category("FileSizeFormatter")]
        public void FileSizeFormatter_ReceivesPositiveValue_ReturnsBytes()
        {
            //arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 50;

            //act
            string result = fileHelper.FileSizeFormatter(rawSize);

            //assert
            Assert.AreEqual("50 B", result);
        }

        [Test]
        [Category("FileSizeFormatter")]
        public void FileSizeFormatter_ReceivesPositiveValue_ReturnsKilobytes()
        {
            //arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 2048;

            //act
            string result = fileHelper.FileSizeFormatter(rawSize);

            //assert
            Assert.AreEqual("2 KB", result);
        }

        [Test]
        [Category("FileSizeFormatter")]
        public void FileSizeFormatter_ReceivesPositiveValue_ReturnsMegabytes()
        {
            //arrange
            SetInvariantCulture();

            FileHelper fileHelper = new FileHelper();

            double rawSize = 2091008;

            //act
            string result = fileHelper.FileSizeFormatter(rawSize);

            //assert
            Assert.AreEqual("1.99 MB", result);

            RestoreCulture();
        }

        [Test]
        [Category("FileSizeFormatter")]
        public void FileSizeFormatter_ReceivesPositiveValue_ReturnsGigabytes()
        {
            //arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 5368709120;

            //act
            string result = fileHelper.FileSizeFormatter(rawSize);

            //assert
            Assert.AreEqual("5 GB", result);
        }

        [Test]
        [Category("FileSizeFormatter")]
        public void FileSizeFormatter_ReceivesNegativeValue_ReturnsEmptyString()
        {
            //arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = -1024;

            //act
            string result = fileHelper.FileSizeFormatter(rawSize);

            //assert
            Assert.AreEqual("", result);
        }

        [Test]
        [Category("FileSizeConverter")]
        public void FileSizeConverter_ReceivesPositiveIntegerSizeAndMegabyteUnit_ReturnsBytes()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 1;

            // act
            double result = fileHelper.FileSizeConverter(rawSize, FileSizeUnit.Megabyte);

            // assert
            Assert.AreEqual(1048576, result);
        }

        [Test]
        [Category("FileSizeConverter")]
        public void FileSizeConverter_ReceivesPositiveDecimalSizeAndMegabyteUnit_ReturnsBytes()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 55.9;

            // act
            double result = fileHelper.FileSizeConverter(rawSize, FileSizeUnit.Megabyte);

            // assert
            Assert.AreEqual(58615398.4, result);
        }

        [Test]
        [Category("FileSizeConverter")]
        public void FileSizeConverter_ReceivesPositiveIntegerSizeAndGigabyteUnit_ReturnsBytes()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 29;

            // act
            double result = fileHelper.FileSizeConverter(rawSize, FileSizeUnit.Gigabyte);

            // assert
            Assert.AreEqual(31138512896, result);
        }

        [Test]
        [Category("FileSizeConverter")]
        public void FileSizeConverter_ReceivesPositiveDecimalSizeAndGigabyteUnit_ReturnsBytes()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 50.5;

            // act
            double result = fileHelper.FileSizeConverter(rawSize, FileSizeUnit.Gigabyte);

            // assert
            Assert.AreEqual(54223962112, result);
        }

        [Test]
        [Category("FileSizeConverter")]
        public void FileSizeConverter_ReceivesNegativeSize_ReturnsZeroBytes()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = -1;

            // act
            double result = fileHelper.FileSizeConverter(rawSize, It.IsAny<FileSizeUnit>());

            // assert
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("FileSizeConverter")]
        public void FileSizeConverter_ReceivesNoneUnit_ReturnsZeroBytes()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 1;

            // act
            double result = fileHelper.FileSizeConverter(rawSize, FileSizeUnit.None);

            // assert
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("FileSizeConverter")]
        public void FileSizeConverter_ReceivesNullUnit_ReturnsZeroBytes()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            double rawSize = 1;

            // act
            double result = fileHelper.FileSizeConverter(rawSize, It.IsAny<FileSizeUnit>());

            // assert
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("GenerateUniqueFileName")]
        public void GenerateUniqueFileName_FileArgumentIsNotSupplied_ThrowsFileArgumentNullException()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            TestDelegate testDelegate = () => fileHelper.GenerateUniqueFileName(null, null, It.IsAny<UniqueCharsPosition>());

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("file"));
        }

        [Test]
        [Category("GenerateUniqueFileName")]
        public void GenerateUniqueFileName_GeneratorArgumentIsNotSupplied_ThrowsGeneratorArgumentNullException()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            AppFile file = new AppFile();

            TestDelegate testDelegate = () => fileHelper.GenerateUniqueFileName(file, null, It.IsAny<UniqueCharsPosition>());

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("generator"));
        }

        [TestCase(UniqueCharsPosition.None, "TestFileName.extension")]
        [TestCase(UniqueCharsPosition.Prefix, "UniqueCharsSequence_TestFileName.extension")]
        [TestCase(UniqueCharsPosition.Suffix, "TestFileName_UniqueCharsSequence.extension")]
        [Category("GenerateUniqueFileName")]
        public void GenerateUniqueFileName_SupplyFileAndGeneratorAndPosition_ReturnsFileNameWithExtensionWithProperlyPositionedUniqueString(UniqueCharsPosition position, string expectedResult)
        {
            // arrange
            AppFile file = new AppFile();
            file.FileNameFull = "TestFileName.extension";
            file.FileNameWithoutExtension = "TestFileName";
            file.FileExtension = ".extension";

            Mock<IUniqueCharsGenerator> generatorMock = new Mock<IUniqueCharsGenerator>();
            generatorMock.Setup(s => s.Generate()).Returns("UniqueCharsSequence");

            FileHelper fileHelper = new FileHelper();

            // act
            string result = fileHelper.GenerateUniqueFileName(file, generatorMock.Object, position);

            //assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [Category("GetRandomFile")]
        public void GetRandomFile_SupplyNullCollection_ThrowsFileArgumentNullException()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            TestDelegate testDelegate = () => fileHelper.GetRandomFile(null);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("files"));
        }

        [Test]
        [Category("GetRandomFile")]
        public void GetRandomFile_SupplyEmptyCollection_ReturnsNullInstance()
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            // act
            AppFile result = fileHelper.GetRandomFile(new List<AppFile>());

            // assert
            Assert.IsNull(result);
        }

        [TestCase(5)]
        [TestCase(100)]
        [TestCase(20000)]
        [Category("GetRandomFile")]
        public void GetRandomFile_SupplyCollection_ReturnsAnyRandomInstance(int filesCount)
        {
            // arrange
            FileHelper fileHelper = new FileHelper();

            List<AppFile> files = new List<AppFile>();

            for (int i = 0; i < filesCount; i++)
            {
                files.Add(new AppFile {});
            }

            // act
            AppFile result = fileHelper.GetRandomFile(files);

            // assert
            Assert.Contains(result, files);
        }
    }
}