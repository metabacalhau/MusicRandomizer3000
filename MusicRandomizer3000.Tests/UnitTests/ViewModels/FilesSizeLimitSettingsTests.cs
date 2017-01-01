using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace MusicRandomizer3000.Tests.UnitTests.ViewModels
{
    [TestFixture]
    public class FilesSizeLimitSettingsTests
    {
        [Test]
        public void Constructor_Parameterless_SelectedSizePropertyReturnsFirstElementOfSizesList()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();

            Assert.AreEqual(sut.Sizes.First(), sut.SelectedSize);
        }

        [Test]
        public void Constructor_Parameterless_SizeLimitPropertyReturnsSelectedSizeMaxValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();

            Assert.AreEqual(sut.SelectedSize.MaxValue, sut.SizeLimit);
        }

        [Test]
        public void Constructor_SupplyNullForSavedSizeParameter_SelectedSizePropertyReturnsFirstElementOfSizesList()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings(null, It.IsAny<double?>());

            Assert.AreEqual(sut.Sizes.First(), sut.SelectedSize);
        }

        [Test]
        public void Constructor_SupplySavedSizeParameter_SelectedSizePropertyReturnsElementOfSizesList()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings(1, It.IsAny<double?>());

            Assert.AreEqual(sut.Sizes.Single(x => x.ID == 1), sut.SelectedSize);
        }

        [Test]
        public void Constructor_SupplyNonExistingSavedSizeParameterInSizesList_SelectedSizePropertyReturnsFirstElementOfSizesList()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings(999, It.IsAny<double?>());

            Assert.AreEqual(sut.Sizes.First(), sut.SelectedSize);
        }

        [Test]
        public void Constructor_SupplyNullForSizeLimitParameter_SizeLimitPropertyReturnsSelectedSizeMaxValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings(It.IsAny<int?>(), null);

            Assert.AreEqual(sut.SelectedSize.MaxValue, sut.SizeLimit);
        }

        [Test]
        public void Constructor_SupplySavedSizeLimitParameter_SizeLimitPropertyReturnsExpectedValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings(It.IsAny<int?>(), 5);

            Assert.AreEqual(5, sut.SizeLimit);
        }

        [Test]
        public void SizeRange_ReturnsExpectedFormattedString()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();

            Assert.AreEqual(string.Format("({0} - {1})", sut.SelectedSize.MinValue, sut.SelectedSize.MaxValue), sut.SizeRange);
        }

        [Test]
        public void SizeLimit_SetValue_FiresPropertyChangedValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();

            sut.ShouldNotifyOn(s => s.SizeLimit).When(s => s.SizeLimit = 2);
        }

        [Test]
        public void SizeLimit_SetSameValue_DoesNotFirePropertyChangedValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();
            sut.SizeLimit = 5;

            sut.ShouldNotNotifyOn(s => s.SizeLimit).When(s => s.SizeLimit = 5);
        }

        [Test]
        public void SizeLimit_SetValueLessThanZero_ReturnsSelectedSizeMinimumValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();
            sut.SizeLimit = -100;

            Assert.AreEqual(sut.SelectedSize.MinValue, sut.SizeLimit);
        }

        [Test]
        public void SizeLimit_SetValueMoreThanMaxValueOfSelectedSize_ReturnsSelectedSizeMaximumValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();
            sut.SizeLimit = 500000;

            Assert.AreEqual(sut.SelectedSize.MaxValue, sut.SizeLimit);
        }

        [Test]
        public void SizeLimit_SetValueBetweenZeroAndSelectedSizeMaxValue_ReturnsExpectedValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();
            sut.SizeLimit = 10;

            Assert.AreEqual(10, sut.SizeLimit);
        }

        [Test]
        public void SelectedSize_SetValue_FiresPropertyChangedValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();

            sut.ShouldNotifyOn(s => s.SelectedSize).When(s => s.SelectedSize = s.Sizes.First(x => x != s.SelectedSize));
        }

        [Test]
        public void SelectedSize_SetValue_FiresSizeRangePropertyChangedValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();

            sut.ShouldNotifyOn(s => s.SizeRange).When(s => s.SelectedSize = s.Sizes.First(x => x != s.SelectedSize));
        }

        [Test]
        public void SelectedSize_SetValue_MustSetSizeLimitToSelectedSizeMaxValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();
            sut.SelectedSize = sut.Sizes.First(x => x != sut.SelectedSize);

            Assert.AreEqual(sut.SelectedSize.MaxValue, sut.SizeLimit);
        }

        [Test]
        public void SelectedSize_SetSameValue_DoesNotFirePropertyChangedValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();

            sut.ShouldNotNotifyOn(s => s.SelectedSize).When(s => s.SelectedSize = s.Sizes.First(x => x == s.SelectedSize));
        }

        [Test]
        public void SelectedSize_SetSameValue_DoesNotFireSizeRangePropertyChangedValue()
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();

            sut.ShouldNotNotifyOn(s => s.SizeRange).When(s => s.SelectedSize = s.Sizes.First(x => x == s.SelectedSize));
        }

        [TestCase(0, 1048576)]
        [TestCase(2, 2097152)]
        [TestCase(1023, 1072693248)]
        [TestCase(1024, 1072693248)]
        public void SizeLimitBytes_SelectedSizeIsMegabytes_ReturnsExpectedValue(double sizeLimit, double expectedValue)
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();
            sut.SelectedSize = sut.Sizes.Single(x => x.ID == 1);
            sut.SizeLimit = sizeLimit;

            double result = sut.SizeLimitBytes;

            Assert.AreEqual(expectedValue, result);
        }

        [TestCase(0, 1073741824)]
        [TestCase(2, 2147483648)]
        [TestCase(10, 10737418240)]
        [TestCase(11, 10737418240)]
        public void SizeLimitBytes_SelectedSizeIsGygabytes_ReturnsExpectedValue(double sizeLimit, double expectedValue)
        {
            FilesSizeLimitSettings sut = new FilesSizeLimitSettings();
            sut.SelectedSize = sut.Sizes.Single(x => x.ID == 2);
            sut.SizeLimit = sizeLimit;

            double result = sut.SizeLimitBytes;

            Assert.AreEqual(expectedValue, result);
        }
    }
}