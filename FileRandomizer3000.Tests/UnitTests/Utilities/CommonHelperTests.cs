using FileRandomizer3000.Core.Utilities;
using NUnit.Framework;
using System.Globalization;
using System.Threading;

namespace FileRandomizer3000.Tests.UnitTests.Utilities
{
    [TestFixture]
    public class CommonHelperTests
    {
        CultureInfo savedCulture;

        private CultureInfo SetCultureInfo(string currentCultureCode)
        {
            savedCulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(currentCultureCode);

            return Thread.CurrentThread.CurrentCulture;
        }

        private void RestoreCultureInfo()
        {
            Thread.CurrentThread.CurrentCulture = savedCulture;
        }

        [TestCase("", "en-US", 0)]
        [TestCase("1.599", "en-US", 1.599)]
        [TestCase("1.599", "ru-RU", 1.599)]
        [TestCase("999.999999", "pt-PT", 999.999999)]
        [TestCase("999.999999", "en-US", 999.999999)]
        public void GetDouble_SupplyString_ReturnsExpectedDoubleValue(string inputValueToFormat, string currentCultureCode, double expectedResult)
        {
            CultureInfo currentCulture = SetCultureInfo(currentCultureCode);

            // act
            double result = CommonHelper.GetDouble(inputValueToFormat, currentCulture);

            // assert
            Assert.AreEqual(expectedResult, result);

            RestoreCultureInfo();
        }
    }
}
