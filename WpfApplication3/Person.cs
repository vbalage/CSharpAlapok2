using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApplication3.Annotations;

namespace WpfApplication3
{
    public class Person : IDataErrorInfo, INotifyPropertyChanged
    {
        private string _name;
        private int _age;
        private string _error;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string this[string columnName]
        {
            get
            {
                ValidateInternal();
                return string.Empty;
            }
        }

        private void ValidateInternal()
        {
            if (string.IsNullOrWhiteSpace(Name) || Age < 0 || Age > 150)
                Error = "Szar";
            else
                Error = string.Empty;
        }

        public string Error
        {
            get { return _error; }
            private set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
