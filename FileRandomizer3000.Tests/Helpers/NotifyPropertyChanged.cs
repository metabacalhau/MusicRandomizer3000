using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FileRandomizer3000.Tests.Helpers
{
    // Taken from http://blog.ploeh.dk/2009/08/06/AFluentInterfaceForTestingINotifyPropertyChanged/
    public static class NotifyPropertyChanged
    {
        public static NotifyExpectation<T> ShouldNotifyOn<T, TProperty>(this T owner, Expression<Func<T, TProperty>> propertyPicker) where T : INotifyPropertyChanged
        {
            return NotifyPropertyChanged.CreateExpectation(owner, propertyPicker, true);
        }

        public static NotifyExpectation<T> ShouldNotNotifyOn<T, TProperty>(this T owner, Expression<Func<T, TProperty>> propertyPicker) where T : INotifyPropertyChanged
        {
            return NotifyPropertyChanged.CreateExpectation(owner, propertyPicker, false);
        }

        private static NotifyExpectation<T> CreateExpectation<T, TProperty>(T owner, Expression<Func<T, TProperty>> pickProperty, bool eventExpected) where T : INotifyPropertyChanged
        {
            string propertyName = ((MemberExpression)pickProperty.Body).Member.Name;
            return new NotifyExpectation<T>(owner, propertyName, eventExpected);
        }
    }
}