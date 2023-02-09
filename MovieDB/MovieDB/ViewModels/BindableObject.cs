using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieDB.ViewModels
{
	public class BindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

