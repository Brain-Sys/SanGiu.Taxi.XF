using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SanGiu.Taxi.Auth;
using SanGiu.Taxi.ViewModels.Messages;
using SanGiu.Taxi.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanGiu.Taxi.ViewModels
{
    public class TaxiDetailViewModel : ApplicationViewModelBase
    {
        CancellationTokenSource tokenSource;

        private TaxiVM currentItem;
        public TaxiVM CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                base.RaisePropertyChanged();
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
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

        private LoginResult currentUser;
        public LoginResult CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                base.RaisePropertyChanged();
            }
        }

        public RelayCommand GetPositionCommand { get; set; }
        public RelayCommand CancelGpsCommand { get; set; }
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand DownloadCommand { get; set; }

        public TaxiDetailViewModel()
        {
            this.CurrentUser = ApplicationContext.Instance.CurrentUser;

            this.GetPositionCommand = new RelayCommand(GetPositionCommandExecute, GetPositionCommandCanExecute);
            this.CancelGpsCommand = new RelayCommand(() =>
            {
                tokenSource.Cancel();
            }, () => { return this.IsBusy; });

            this.PrintCommand = new RelayCommand(PrintCommandExecute);
            this.DownloadCommand = new RelayCommand(() =>
            {
                ShowDialogMessage msg = new ShowDialogMessage();
                msg.Title = "Download in corso...";
                msg.Message = "...prego attendere...";
                Messenger.Default.Send(msg);
            });
        }

        private void PrintCommandExecute()
        {
            ShowDialogMessage msg = new ShowDialogMessage();
            msg.Title = "PDF Ok!";
            msg.Message = "Report generato!";
            Messenger.Default.Send(msg);

            // this.MessengerInstance.Send(msg);
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
                await Task.Delay(2000);
#endif
                tokenSource = new CancellationTokenSource();
                Position position = await CrossGeolocator.Current
                    .GetPositionAsync(TimeSpan.FromMinutes(1), tokenSource.Token);
                this.Latitude = position.Latitude;
                this.Longitude = position.Longitude;

                await getAddressInfo();
                //var addresses = await CrossGeolocator.Current.GetAddressesForPositionAsync(position, "");
                //foreach (var item in addresses)
                //{
                //    Debug.WriteLine(item.ToString());
                //}
            }

            this.IsBusy = false;
            this.GetPositionCommand.RaiseCanExecuteChanged();
        }

        private async Task getAddressInfo()
        {
            string stringLatitude = latitude.ToString().Replace(',', '.');
            string stringLongitude = longitude.ToString().Replace(',', '.');
            string url = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", stringLatitude, stringLongitude);

            using (HttpClient client = new HttpClient())
            {
                string xml = await client.GetStringAsync(url);
            }
        }
    }
}