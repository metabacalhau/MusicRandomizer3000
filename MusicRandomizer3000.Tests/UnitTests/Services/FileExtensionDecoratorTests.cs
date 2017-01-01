using MusicRandomizer3000.Core.Services;
using MusicRandomizer3000.Core.Services.Interfaces;
using NUnit.Framework;

namespace MusicRandomizer3000.Tests.UnitTests.Services
{
    [TestFixture]
    public class FileExtensionDecoratorTests
    {
        private class AllowedExtensionsDecorator : FileExtensionDecorator
        {
            public AllowedExtensionsDecorator() : this(null) { }
            public AllowedExtensionsDecorator(IFileExtension additionalExtensions)
                : base(additionalExtensions)
            {
                Allowed = new string[] { "*.ext1", "*.ext2" };
            }
        }

        private class AdditionalExtensionsDecorator : FileExtensionDecorator
        {
            public AdditionalExtensionsDecorator() : this(null) { }
            public AdditionalExtensionsDecorator(IFileExtension additionalExtensions)
                : base(additionalExtensions)
            {
                Allowed = new string[] { "*.add_ext1", "*.add_ext2" };
            }
        }

        private class EmptyExtensionsDecorator : FileExtensionDecorator
        {
            public EmptyExtensionsDecorator() : this(null) { }
            public EmptyExtensionsDecorator(IFileExtension additionalExtensions)
                : base(additionalExtensions)
            {
                Allowed = null;
            }
        }

        [Test]
        public void Contains_CreateDecoratorInstanceWithAllowedExtensions_ContainsAllowedExtensions()
        {
            // arrange
            AllowedExtensionsDecorator allowedExtensions = new AllowedExtensionsDecorator();

            // act, assert
            Assert.IsTrue(allowedExtensions.Contains("*.ext1"));
        }

        [Test]
        public void Contains_CreateDecoratorInstanceWithAllowedExtensions_DoesntContainAllowedExtensions()
        {
            // arrange
            AllowedExtensionsDecorator allowedExtensions = new AllowedExtensionsDecorator();

            // act, assert
            Assert.IsFalse(allowedExtensions.Contains("*.non_esisting_extension"));
        }

        [Test]
        public void Contains_CreateDecoratorInstanceWithAllowedExtensionsAndInjectAdditionalExtensions_ContainsAdditionalExtensions()
        {
            // arrange
            AdditionalExtensionsDecorator additionalExtensions = new AdditionalExtensionsDecorator();
            AllowedExtensionsDecorator allowedExtensions = new AllowedExtensionsDecorator(additionalExtensions);

            // act, assert
            foreach (string extension in additionalExtensions.AllowedExtensions)
            {
                Assert.IsTrue(allowedExtensions.Contains(extension));
            }
        }

        [Test]
        public void AllowedExtensions_CreateDecoratorWithEmptyEntensions_AllowedExtensionsAreNull()
        {
            // arrange
            EmptyExtensionsDecorator emptyExtensions = new EmptyExtensionsDecorator();

            // act, assert
            Assert.IsNull(emptyExtensions.AllowedExtensions);
        }
    }
}