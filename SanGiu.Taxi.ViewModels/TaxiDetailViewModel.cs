﻿using GalaSoft.MvvmLight.Command;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SanGiu.Taxi.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanGiu.Taxi.ViewModels
{
    public class TaxiDetailViewModel : ApplicationViewModelBase
    {
        private TaxiVM currentItem;
        public TaxiVM CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value;
                base.RaisePropertyChanged();
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value;
                base.RaisePropertyChanged();
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                base.RaisePropertyChanged();
            }
        }

        public RelayCommand GetPositionCommand { get; set; }

        public TaxiDetailViewModel()
        {
            this.GetPositionCommand = new RelayCommand(GetPositionCommandExecute, GetPositionCommandCanExecute);
        }

        private bool GetPositionCommandCanExecute()
        {
            return !this.IsBusy;
        }

        public override void Init()
        {
            base.Init();
            this.CurrentItem = base.Parameter as TaxiVM;
        }

        private async void GetPositionCommandExecute()
        {
            if (!CrossGeolocator.IsSupported) return;

            this.IsBusy = true;
            this.GetPositionCommand.RaiseCanExecuteChanged();

            if (CrossGeolocator.Current.IsGeolocationAvailable)
            {
#if DEBUG
                await Task.Delay(10000);
#endif

                Position position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromMinutes(1));

                this.Latitude = position.Latitude;
                this.Longitude = position.Longitude;
            }

            this.IsBusy = false;
            this.GetPositionCommand.RaiseCanExecuteChanged();
        }
    }
}