using System;
using System.ComponentModel;

namespace FileRandomizer3000.Core.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual bool CheckPropertyChanged<T>(string propertyName, ref T oldValue, ref T newValue)
        {
            if (oldValue == null && newValue == null)
            {
                return false;
            }

            if ((oldValue == null && newValue != null) || !oldValue.Equals((T)newValue))
            {
                oldValue = newValue;

                return true;
            }

            return false;
        }

        public virtual void FirePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }
    }
}