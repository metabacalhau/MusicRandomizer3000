using NUnit.Framework;
using System;
using System.ComponentModel;

namespace FileRandomizer3000.Tests.Helpers
{
    // Taken from http://blog.ploeh.dk/2009/08/06/AFluentInterfaceForTestingINotifyPropertyChanged/
    public class NotifyExpectation<T> where T : INotifyPropertyChanged
    {
        private readonly T owner;
        private readonly string propertyName;
        private readonly bool eventExpected;

        public NotifyExpectation(T owner, string propertyName, bool eventExpected)
        {
            this.owner = owner;
            this.propertyName = propertyName;
            this.eventExpected = eventExpected;
        }

        public void When(Action<T> action)
        {
            bool eventWasRaised = false;

            this.owner.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == this.propertyName)
                {
                    eventWasRaised = true;
                }
            };

            action(this.owner);

            Assert.AreEqual(this.eventExpected, eventWasRaised, "PropertyChanged on {0}", this.propertyName);
        }
    }
}