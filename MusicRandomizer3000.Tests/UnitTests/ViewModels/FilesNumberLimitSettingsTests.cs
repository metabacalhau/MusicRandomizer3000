using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Tests.Helpers;
using NUnit.Framework;

namespace MusicRandomizer3000.Tests.UnitTests.ViewModels
{
    [TestFixture]
    public class FilesNumberLimitSettingsTests
    {
        [Test]
        public void Constructor_Parameterless_NumberMustReturnMinimumValue()
        {
            FilesNumberLimitSettings sut = new FilesNumberLimitSettings();

            Assert.AreEqual(sut.Minimum, sut.Number);
        }

        [Test]
        public void Constructor_SupplyNullForSavedNumberParameter_NumberReturnsMinimumValue()
        {
            FilesNumberLimitSettings sut = new FilesNumberLimitSettings(null);

            Assert.AreEqual(sut.Minimum, sut.Number);
        }

        [Test]
        public void Number_SetValue_FiresPropertyChangedValue()
        {
            FilesNumberLimitSettings sut = new FilesNumberLimitSettings();

            sut.ShouldNotifyOn(s => s.Number).When(s => s.Number = 2);
        }

        [Test]
        public void Number_SetSameValue_DoesNotFirePropertyChangedValue()
        {
            FilesNumberLimitSettings sut = new FilesNumberLimitSettings();
            sut.Number = 5;

            sut.ShouldNotNotifyOn(s => s.Number).When(s => s.Number = 5);
        }

        [Test]
        public void Number_SetValueInRange_ReturnsExpectedValue()
        {
            FilesNumberLimitSettings sut = new FilesNumberLimitSettings();
            sut.Number = 5;

            Assert.AreEqual(5, sut.Number);
        }

        [Test]
        public void Number_SetValueLessThanMinimum_ReturnsMinimumValue()
        {
            FilesNumberLimitSettings sut = new FilesNumberLimitSettings();
            sut.Number = -5;

            Assert.AreEqual(sut.Minimum, sut.Number);
        }

        [Test]
        public void Number_SetValueMoreThanMaximum_ReturnsMaximumValue()
        {
            FilesNumberLimitSettings sut = new FilesNumberLimitSettings();
            sut.Number = 200000;

            Assert.AreEqual(sut.Maximum, sut.Number);
        }

        [Test]
        public void Range_ReturnsExpectedFormattedString()
        {
            FilesNumberLimitSettings sut = new FilesNumberLimitSettings();

            Assert.AreEqual(string.Format("({0} - {1})", sut.Minimum, sut.Maximum), sut.Range);
        }
    }
}
