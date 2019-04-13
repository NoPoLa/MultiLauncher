using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MultiLauncher.UI
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly object _propertyLock = new object();

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Methods

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool SetProperty<T>(ref T property, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
                return false;

            property = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        public async Task RunCommandAsync(Expression<Func<bool>> flag, Func<Task> action)
        {
            lock (_propertyLock)
            {
                if (!flag.GetPropertyValue())
                    return;

                flag.SetPropertyValue(true);
            }
            try
            {
                await action?.Invoke();
            }
            finally
            {
                flag.SetPropertyValue(false);
            }
        }

        public async Task<T> RunCommandAsync<T>(Expression<Func<bool>> flag, Func<Task<T>> action, T defaultValue = default(T))
        {
            lock (_propertyLock)
            {
                if (!flag.GetPropertyValue())
                    return defaultValue;

                flag.SetPropertyValue(true);
            }
            try
            {
                return await action?.Invoke();
            }
            finally
            {
                flag.SetPropertyValue(false);
            }
        }

        #endregion

        #region Private Methods

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private void VerifyPropertyName(string propertyName)
        {
            var property = this.GetType().GetMember(propertyName);

            if (property is null)
                Debug.Fail($"Invalid property name. Type: {GetType()}, Name: {propertyName}");
        }

        #endregion
    }
}
