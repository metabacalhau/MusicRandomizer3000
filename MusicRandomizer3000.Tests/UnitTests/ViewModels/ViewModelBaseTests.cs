using MusicRandomizer3000.Core.ViewModels;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MusicRandomizer3000.Tests.UnitTests.ViewModels
{
    [TestFixture]
    public class ViewModelBaseTests
    {
        private class TestClass : ViewModelBase
        {
            private string singleTestProperty;
            public string SingleCallProperty
            {
                get
                {
                    return singleTestProperty;
                }
                set
                {
                    if (CheckPropertyChanged<string>("SingleCallProperty", ref singleTestProperty, ref value))
                    {
                        FirePropertyChanged("SingleCallProperty");
                    }
                }
            }

            private string multipleCallsProperty;
            public string MultipleCallsProperty
            {
                get
                {
                    return multipleCallsProperty;
                }
                set
                {
                    if (CheckPropertyChanged<string>("MultipleCallsProperty", ref multipleCallsProperty, ref value))
                    {
                        FirePropertyChanged("MultipleCallsProperty");
                        FirePropertyChanged("SingleCallProperty");
                    }
                }
            }
        }

        [TestCase("foo", "bar", "bar")]
        [TestCase("baz", "baz", "baz")]
        [TestCase("-100", "-10", "-10")]
        [TestCase(null, null, null)]
        public void SetPropertyValue_UpdatesPropertyWithExpectedValue(string initialValue, string newValue, string expectedValue)
        {
            // arrange
            TestClass sut = new TestClass();
            sut.SingleCallProperty = initialValue;

            // act
            sut.SingleCallProperty = newValue;

            // assert
            Assert.AreEqual(expectedValue, sut.SingleCallProperty);
        }

        [Test]
        public void FirePropertyChanged_SetDistinctPropertyValue_FiresPropertyChangedEventOnlyOnesAndWithExpectedPropertyName()
        {
            // arrange
            List<string> receivedEvents = new List<string>();

            TestClass sut = new TestClass();

            sut.PropertyChanged += (sender, e) =>
            {
                receivedEvents.Add(e.PropertyName);
            };

            // act
            sut.SingleCallProperty = "4";

            // assert
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("SingleCallProperty", receivedEvents[0]);
        }

        [Test]
        public void FirePropertyChanged_SetDistinctPropertyValue_FiresPropertyChangedEventTwiceAndWithExpectedPropertyNames()
        {
            // arrange
            List<string> receivedEvents = new List<string>();

            TestClass sut = new TestClass();

            sut.PropertyChanged += (sender, e) =>
            {
                receivedEvents.Add(e.PropertyName);
            };

            // act
            sut.MultipleCallsProperty = "4";

            // assert
            Assert.AreEqual(2, receivedEvents.Count);
            Assert.AreEqual("MultipleCallsProperty", receivedEvents[0]);
            Assert.AreEqual("SingleCallProperty", receivedEvents[1]);
        }

        [Test]
        public void FirePropertyChanged_SetSamePropertyValue_DoesntFirePropertyChangedEvent()
        {
            // arrange
            List<string> receivedEvents = new List<string>();

            TestClass sut = new TestClass();
            sut.SingleCallProperty = "4";

            sut.PropertyChanged += (sender, e) =>
            {
                receivedEvents.Add(e.PropertyName);
            };

            // act
            sut.SingleCallProperty = "4";

            // assert
            Assert.AreEqual(0, receivedEvents.Count);
        }
    }
}