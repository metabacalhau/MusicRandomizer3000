using MusicRandomizer3000.Core.Services;
using NUnit.Framework;
using System;

namespace MusicRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class GuidCharactersGeneratorTests
    {
        [TestCase(-100)]
        [TestCase(0)]
        [TestCase(33)]
        [TestCase(100)]
        public void GuidCharactersGenerator_SupplyInvalidMaxLength_ThrowsArgumentOutOfRangeException(int maxLength)
        {
            // arrange
            TestDelegate testDelegate = () => new GuidCharactersGenerator(maxLength);

            // act, assert
            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [TestCase(5)]
        [TestCase(10)]
        [TestCase(32)]
        public void Generate_SupplyMaxLength_GeneratesStringOfSpecifiedMaximumLength(int maxLength)
        {
            // arrange
            GuidCharactersGenerator generator = new GuidCharactersGenerator(maxLength);

            // act
            string result = generator.Generate();

            // assert
            Assert.AreEqual(maxLength, result.Length);
        }
    }
}