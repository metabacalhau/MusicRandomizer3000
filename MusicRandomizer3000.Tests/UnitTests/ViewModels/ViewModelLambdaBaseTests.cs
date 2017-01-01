using MusicRandomizer3000.Core.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicRandomizer3000.Tests.UnitTests.ViewModels
{
    [TestFixture]
    public class ViewModelLambdaBaseTests
    {
        private class TestClass : ViewModelLambdaBase
        {
            private string firstProperty;
            public string FirstProperty
            {
                get
                {
                    return firstProperty;
                }
                set
                {
                    if (CheckPropertyChanged<string>(() => FirstProperty, ref firstProperty, ref value))
                    {
                        FirePropertyChanged(() => FirstProperty);
                    }
                }
            }

            private string secondProperty;
            public string SecondProperty
            {
                get
                {
                    return secondProperty;
                }
                set
                {
                    if (CheckPropertyChanged<string>(() => SecondProperty, ref secondProperty, ref value))
                    {
                        FirePropertyChanged(() => SecondProperty);
                        FirePropertyChanged(() => FirstProperty);
                    }
                }
            }
        }

        [Test]
        public void CheckPropertyChanged_SetPropertyValue_FiresPropertyChangedEventOnceWithExpectedPropertyName()
        {
            // arrange
            List<string> receivedEvents = new List<string>();

            TestClass sut = new TestClass();

            sut.PropertyChanged += (sender, e) =>
            {
                receivedEvents.Add(e.PropertyName);
            };

            // act
            sut.FirstProperty = "4";

            // assert
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("FirstProperty", receivedEvents[0]);
        }

        [Test]
        public void CheckPropertyChanged_SetPropertyValue_FiresPropertyChangedEventTwiceAndWithExpectedPropertyNames()
        {
            // arrange
            List<string> receivedEvents = new List<string>();

            TestClass sut = new TestClass();

            sut.PropertyChanged += (sender, e) =>
            {
                receivedEvents.Add(e.PropertyName);
            };

            // act
            sut.SecondProperty = "4";

            // assert
            Assert.AreEqual(2, receivedEvents.Count);
            Assert.AreEqual("SecondProperty", receivedEvents[0]);
            Assert.AreEqual("FirstProperty", receivedEvents[1]);
        }
    }
}