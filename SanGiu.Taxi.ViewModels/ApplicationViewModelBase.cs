using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.ViewModels
{
    public class ApplicationViewModelBase : ViewModelBase
    {
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value;
                base.RaisePropertyChanged();
            }
        }

        private object parameter;
        public object Parameter
        {
            get { return parameter; }
            set { parameter = value;
                base.RaisePropertyChanged();
            }
        }
    }
}