using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace FileRandomizer3000.Tests.UnitTests.ViewModels
{
    [TestFixture]
    public class FilesAndFoldersLimitSettingsTests
    {
        [Test]
        public void Constructor_Parameterless_FoldersNumberMustReturnMinimumValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();

            Assert.AreEqual(sut.FoldersNumberMinimum, sut.FoldersNumber);
        }

        [Test]
        public void Constructor_Parameterless_FilesNumberMustReturnMinimumValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();

            Assert.AreEqual(sut.FilesNumberMinimum, sut.FilesNumber);
        }

        [Test]
        public void Constructor_SupplyNullForFoldersNumberParameter_FoldersNumberReturnsMinimumValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings(null, It.IsAny<int?>(), It.IsAny<bool>());

            Assert.AreEqual(sut.FoldersNumberMinimum, sut.FoldersNumber);
        }

        [Test]
        public void Constructor_SupplyValueForFoldersNumberParameter_FoldersNumberReturnsExpectedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings(44, It.IsAny<int?>(), It.IsAny<bool>());

            Assert.AreEqual(sut.FoldersNumber, sut.FoldersNumber);
        }

        [Test]
        public void Constructor_SupplyNullForFilesNumberParameter_FilesNumberReturnsMinimumValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings(It.IsAny<int?>(), null, It.IsAny<bool>());

            Assert.AreEqual(sut.FilesNumberMinimum, sut.FilesNumber);
        }

        [Test]
        public void Constructor_SupplyValueForFilesNumberParameter_FilesNumberReturnsExpectedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings(It.IsAny<int?>(), 34, It.IsAny<bool>());

            Assert.AreEqual(sut.FilesNumber, sut.FilesNumber);
        }

        [Test]
        public void Constructor_SupplyOrganizeFilesParameter_OrganizeFilesReturnsExpectedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings(It.IsAny<int?>(), It.IsAny<int?>(), true);

            Assert.IsTrue(sut.OrganizeFiles);
        }

        [Test]
        public void FoldersNumber_SetValue_FiresPropertyChangedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();

            sut.ShouldNotifyOn(s => s.FoldersNumber).When(s => s.FoldersNumber = 2);
        }

        [Test]
        public void FoldersNumber_SetSameValue_DoesNotFirePropertyChangedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.FoldersNumber = 1;

            sut.ShouldNotNotifyOn(s => s.FoldersNumber).When(s => s.FoldersNumber = 1);
        }

        [Test]
        public void FoldersNumber_SetValueLessThanMinimum_ReturnsMinimumValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.FoldersNumber = -1;

            Assert.AreEqual(sut.FoldersNumberMinimum, sut.FoldersNumber);
        }

        [Test]
        public void FoldersNumber_SetValueMoreThanMaximum_ReturnsMaximumValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.FoldersNumber = 1000;

            Assert.AreEqual(sut.FoldersNumberMaximum, sut.FoldersNumber);
        }

        [Test]
        public void FoldersNumber_SetValueInRange_ReturnsExpectedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.FoldersNumber = 2;

            Assert.AreEqual(2, sut.FoldersNumber);
        }

        [Test]
        public void FilesNumber_SetValue_FiresPropertyChangedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();

            sut.ShouldNotifyOn(s => s.FilesNumber).When(s => s.FilesNumber = 2);
        }

        [Test]
        public void FilesNumber_SetSameValue_DoesNotFirePropertyChangedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.FilesNumber = 1;

            sut.ShouldNotNotifyOn(s => s.FilesNumber).When(s => s.FilesNumber = 1);
        }

        [Test]
        public void FilesNumber_SetValueLessThanMinimum_ReturnsMinimumValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.FilesNumber = -1;

            Assert.AreEqual(sut.FilesNumberMinimum, sut.FilesNumber);
        }

        [Test]
        public void FilesNumber_SetValueMoreThanMaximum_ReturnsMaximumValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.FilesNumber = 1000;

            Assert.AreEqual(sut.FilesNumberMaximum, sut.FilesNumber);
        }

        [Test]
        public void FilesNumber_SetValueInRange_ReturnsExpectedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.FilesNumber = 55;

            Assert.AreEqual(55, sut.FilesNumber);
        }

        [Test]
        public void OrganizeFiles_SetValue_FiresPropertyChangedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();

            sut.ShouldNotifyOn(s => s.OrganizeFiles).When(s => s.OrganizeFiles = true);
        }

        [Test]
        public void OrganizeFiles_SetSameValue_DoesNotFirePropertyChangedValue()
        {
            FilesAndFoldersLimitSettings sut = new FilesAndFoldersLimitSettings();
            sut.OrganizeFiles = false;

            sut.ShouldNotNotifyOn(s => s.OrganizeFiles).When(s => s.OrganizeFiles = false);
        }
    }
}