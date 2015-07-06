using System;
using System.Linq.Expressions;

namespace FileRandomizer3000.Core.ViewModels
{
    /// <summary>
    /// Taken from: http://www.limilabs.com/blog/inotifypropertychanged-with-lambdas
    /// </summary>
    public class ViewModelLambdaBase : ViewModelBase
    {
        protected bool CheckPropertyChanged<T>(Expression<Func<object>> expression, ref T oldValue, ref T newValue)
        {
            string propertyName = PropertyName.For(expression);

            return CheckPropertyChanged(propertyName, ref oldValue, ref newValue);
        }

        protected void FirePropertyChanged(Expression<Func<object>> expression)
        {
            string propertyName = PropertyName.For(expression);

            base.FirePropertyChanged(propertyName);
        }
    };
}