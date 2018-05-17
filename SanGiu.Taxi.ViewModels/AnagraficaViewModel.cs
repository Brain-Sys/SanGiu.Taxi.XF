using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SanGiu.Taxi.Interfaces;
using SanGiu.Taxi.Repo;
using SanGiu.Taxi.ViewModels.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
//using d = SanGiu.Taxi.DomainModel;
using System.Collections.ObjectModel;
using SanGiu.Taxi.ViewModels.VM;

namespace SanGiu.Taxi.ViewModels
{
    public class AnagraficaViewModel : ApplicationViewModelBase
    {
        TaxiRepository repo;

        private ObservableCollection<TaxiVM> items;
        public ObservableCollection<TaxiVM> Items
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

        private TaxiVM selectedTaxi;
        public TaxiVM SelectedTaxi
        {
            get { return selectedTaxi; }
            set
            {
                selectedTaxi = value;
                base.RaisePropertyChanged();
                showAutista();
            }
        }

        public RelayCommand<TaxiVM> UpdateCommand { get; set; }
        public RelayCommand<int> RemoveCommand { get; set; }

        public AnagraficaViewModel()
        {
            this.UpdateCommand = new RelayCommand<TaxiVM>(UpdateCommandExecute, UpdateCommandCanExecute);
            this.RemoveCommand = new RelayCommand<int>(RemoveCommandExecute);

            var msg = new DependencyMessage<IStorage>();
            msg.Resolved = initRepository;
            Messenger.Default.Send(msg);
        }

        private void RemoveCommandExecute(int id)
        {
            QuestionMessage msg = new QuestionMessage();
            msg.Message = "Sei sicuro di voler eliminare?";
            msg.Yes = () => {
                var taxi = this.Items.FirstOrDefault(i => i.Id == id);

                if (taxi != null)
                {
                    this.Items.Remove(taxi);
                }
            };

            msg.No = null;

            Messenger.Default.Send<QuestionMessage>(msg);
        }

        private bool UpdateCommandCanExecute(TaxiVM instance)
        {
            return true;
        }

        private async void UpdateCommandExecute(TaxiVM instance)
        {
            this.IsBusy = true;

            await Task.Run(() => {
                repo.Update(instance.InternalInstance);
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
            var filename = resolver.GetDbDirectory() + "taxi.db";

            repo = new TaxiRepository(filename);
            repo.CheckDB();

            var list = repo.GetTaxiListForHomePage()
                .Select(t => new TaxiVM(t));

            this.Items = new ObservableCollection<TaxiVM>(list);
        }
    }
}