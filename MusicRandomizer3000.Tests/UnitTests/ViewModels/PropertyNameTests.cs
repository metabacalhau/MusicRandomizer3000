using MusicRandomizer3000.Core.ViewModels;
using NUnit.Framework;
using System;

namespace MusicRandomizer3000.Tests.UnitTests.ViewModels
{
    [TestFixture]
    public class PropertyNameTests
    {
        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Home Home { get; set; }
        }

        private class Home
        {
            public string City { get; set; }
            public string FlatNumber { get; set; }
        }

        public string NameForTest { get; set; }

        [Test]
        public void ForUntyped_SupplyInvalidExpression_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => PropertyName.For(null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("expression"));
        }

        [Test]
        public void ForTyped_SupplyInvalidExpression_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => PropertyName.For<Person>(null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("expression"));
        }

        [Test]
        public void GetMemberName_SupplyInvalidExpression_ThrowsArgumentNullException()
        {
            TestDelegate testDelegate = () => PropertyName.GetMemberName(null);

            Assert.That(testDelegate, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("expression"));
        }

        [Test]
        public void CanGetPropertyName_SameType_UsingLambda()
        {
            Assert.AreEqual("NameForTest", PropertyName.For(() => NameForTest));
        }

        [Test]
        public void CanGetPropertyName_UsingLambda()
        {
            Assert.AreEqual("Name", PropertyName.For<Person>(x => x.Name));
            Assert.AreEqual("Age", PropertyName.For<Person>(x => x.Age));
        }

        [Test]
        public void CanGetPropertyName_Composite_UsingLambda()
        {
            Assert.AreEqual("Home.City", PropertyName.For<Person>(x => x.Home.City));
            Assert.AreEqual("Home.FlatNumber", PropertyName.For<Person>(x => x.Home.FlatNumber));
        }
    }
}