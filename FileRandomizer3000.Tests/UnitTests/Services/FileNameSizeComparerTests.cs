using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services;
using NUnit.Framework;
using System;

namespace FileRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class FileNameSizeComparerTests
    {
        FileNameSizeComparer comparer = null;

        [SetUp]
        public void SetUp()
        {
            comparer = new FileNameSizeComparer();
        }

        [Test]
        public void Equals_CompareSameInstance_ReturnsTrue()
        {
            // arrange
            AppFile file = new AppFile();

            // act
            bool result = comparer.Equals(file, file);

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_CompareNullInstanceToNonNullInstance_ReturnsFalse()
        {
            // arrange
            AppFile file = new AppFile();

            // act
            bool result = comparer.Equals(null, file);

            // assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_CompareNonNullInstanceToNullInstance_ReturnsFalse()
        {
            // arrange
            AppFile file = new AppFile();

            // act
            bool result = comparer.Equals(file, null);

            // assert
            Assert.IsFalse(result);
        }

        [TestCase("a", "a", 0.1, 0.1, true)]
        [TestCase("a", "b", 0.1, 0.1, false)]
        [TestCase("a", "a", 0.2, 0.1, false)]
        public void Equals_CompareInstancesByTheirFileNameFullAndFileSizeProperties_ReturnsExpectedResult(string fileNameFullOne, string fileNameFullTwo, double fileSizeOne, double fileSizeTwo, bool expectedResult)
        {
            // arrange
            AppFile fileOne = new AppFile
            {
                FileNameFull = fileNameFullOne,
                FileSize = fileSizeOne
            };

            AppFile fileTwo = new AppFile
            {
                FileNameFull = fileNameFullTwo,
                FileSize = fileSizeTwo
            };

            // act
            bool result = comparer.Equals(fileOne, fileTwo);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetHashCode_SupplyNullObject_ThrowsArgumentNullException()
        {
            // arrange
            AppFile file = null;

            TestDelegate testDelegate = () => comparer.GetHashCode(file);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("obj"));
        }

        [TestCase("file1", 0.1)]
        [TestCase("file2", 2)]
        public void GetHashCode_ExecuteFunctionForTwoObjectsWithTheSameProperties_ReturnsEqualHashcodes(string fileNameFull, double fileSize)
        {
            // arrange
            AppFile fileOne = new AppFile
            {
                FileNameFull = fileNameFull,
                FileSize = fileSize
            };

            AppFile fileTwo = new AppFile
            {
                FileNameFull = fileNameFull,
                FileSize = fileSize
            };

            // act
            int fileOneHashCode = comparer.GetHashCode(fileOne);
            int fileTwoHashCode = comparer.GetHashCode(fileTwo);

            // assert
            Assert.AreEqual(fileOneHashCode, fileTwoHashCode);
        }

        [TestCase("file1", 0.1, "file2", 0.2)]
        [TestCase("file1", 0.2, "file2", 0.2)]
        [TestCase("file1", 0.2, "file1", 3)]
        [TestCase("file1", 0.222222, "file1", 0.33333333)]
        [TestCase("file2", 99, "file1", 99)]
        [TestCase("file2", 99, "", 99)]
        [TestCase("file2", 99, null, 99)]
        [TestCase("", 991, "", 992)]
        [TestCase(null, 991, null, 992)]
        public void GetHashCode_ExecuteFunctionForTwoObjectsWithDifferentProperties_ReturnsDifferentHashcodes(string fileNameFullOne, double fileSizeOne, string fileNameFullTwo, double fileSizeTwo)
        {
            // arrange
            AppFile fileOne = new AppFile
            {
                FileNameFull = fileNameFullOne,
                FileSize = fileSizeOne
            };

            AppFile fileTwo = new AppFile
            {
                FileNameFull = fileNameFullTwo,
                FileSize = fileSizeTwo
            };

            // act
            int fileOneHashCode = comparer.GetHashCode(fileOne);
            int fileTwoHashCode = comparer.GetHashCode(fileTwo);

            // assert
            Assert.AreNotEqual(fileOneHashCode, fileTwoHashCode);
        }
    }
}