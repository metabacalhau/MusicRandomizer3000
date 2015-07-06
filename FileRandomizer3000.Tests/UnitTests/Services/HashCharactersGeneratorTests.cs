using FileRandomizer3000.Core.Services;
using NUnit.Framework;

namespace FileRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class HashCharactersGeneratorTests
    {
        [Test]
        public void Generate_GeneratesStringWithLengthGreaterThanZero()
        {
            // arrange
            HashCharactersGenerator generator = new HashCharactersGenerator();

            // act
            string result = generator.Generate();

            // assert
            Assert.Greater(result.Length, 0);
        }
    }
}