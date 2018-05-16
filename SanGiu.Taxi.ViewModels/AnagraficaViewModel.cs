using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SanGiu.Taxi.Interfaces;
using SanGiu.Taxi.Repo;
using SanGiu.Taxi.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using d = SanGiu.Taxi.DomainModel;
namespace SanGiu.Taxi.ViewModels
{
    public class AnagraficaViewModel : ApplicationViewModelBase
    {
        TaxiRepository repo;

        private List<d.Taxi> items;
        public List<d.Taxi> Items
        {
            get { return items; }
            set
            {
                if (items != value)
                {
                    items = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        private d.Taxi selectedTaxi;
        public d.Taxi SelectedTaxi
        {
            get { return selectedTaxi; }
            set
            {
                selectedTaxi = value;
                base.RaisePropertyChanged();
                showAutista();
            }
        }

        public RelayCommand<d.Taxi> UpdateCommand { get; set; }

        public AnagraficaViewModel()
        {
            this.UpdateCommand = new RelayCommand<d.Taxi>(UpdateCommandExecute, UpdateCommandCanExecute);

            var msg = new DependencyMessage<IStorage>();
            msg.Resolved = initRepository;
            Messenger.Default.Send(msg);
        }

        private bool UpdateCommandCanExecute(d.Taxi instance)
        {
            return true;
        }

        private async void UpdateCommandExecute(d.Taxi instance)
        {
            this.IsBusy = true;

            await Task.Run(() => {
                repo.Update(instance);
            });

            this.IsBusy = false;
        }

        private void showAutista()
        {
            if (this.SelectedTaxi == null) return;

            var msg = new ShowDialogMessage();
            msg.Title = "Info taxi";
            msg.Message = this.SelectedTaxi.Autista;
            Messenger.Default.Send(msg);
        }

        private void initRepository(IStorage resolver)
        {
            var filename = resolver.GetDbDirectory() +
                    "taxi.db";

            repo = new TaxiRepository(filename);
            repo.CheckDB();
            this.Items = repo.GetTaxiListForHomePage();
        }
    }
}