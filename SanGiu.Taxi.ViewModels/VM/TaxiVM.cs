using GalaSoft.MvvmLight;
using SanGiu.Taxi.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.ViewModels.VM
{
    /// <summary>
    /// A wrapper for Taxi from the domain model
    /// </summary>
    public class TaxiVM : ViewModelBase
    {
        public SanGiu.Taxi.DomainModel.Taxi InternalInstance { get; private set; }

        public int Id
        {
            get
            {
                return InternalInstance.Id;
            }
            set
            {
                InternalInstance.Id = value;
                base.RaisePropertyChanged();
            }
        }

        public string Targa
        {
            get
            {
                return InternalInstance.Targa;
            }
            set
            {
                InternalInstance.Targa = value;
                base.RaisePropertyChanged();
            }
        }

        public int Km
        {
            get
            {
                return InternalInstance.Km;
            }
            set
            {
                InternalInstance.Km = value;
                base.RaisePropertyChanged();
                base.RaisePropertyChanged(nameof(Status));
            }
        }

        public string Autista
        {
            get
            {
                return InternalInstance.Autista;
            }
            set
            {
                InternalInstance.Autista = value;
                base.RaisePropertyChanged();
            }
        }

        public double Prezzo
        {
            get
            {
                return InternalInstance.Prezzo;
            }
            set
            {
                InternalInstance.Prezzo = value;
                base.RaisePropertyChanged();
            }
        }

        public TaxiStatus Status
        {
            get
            {
                return InternalInstance.Status;
            }
        }

        public TaxiVM(SanGiu.Taxi.DomainModel.Taxi taxi)
        {
            this.InternalInstance = taxi;
        }
    }
}