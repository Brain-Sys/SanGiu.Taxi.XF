using GalaSoft.MvvmLight.Messaging;
using SanGiu.Taxi.Interfaces;
using SanGiu.Taxi.ViewModels;
using SanGiu.Taxi.ViewModels.Messages;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SanGiu.Taxi.XF
{
    public partial class App : Application
    {
        public App()
        {
            configureMessages();
            InitializeComponent();

            // this.MainPage = new NavigationPage(new MainPage());
            this.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void configureMessages()
        {
            Messenger.Default.Register<ShowDialogMessage>(this, showMsg);
            Messenger.Default.Register<QuestionMessage>(this, (msg) =>
            {
                Debug.WriteLine(msg.Message);
            });

            Messenger.Default.Register<QuestionMessage>(this, makeQuestion);
            Messenger.Default.Register<OpenViewMessage>(this, openView);
            Messenger.Default.Register<DependencyMessage<IStorage>>(this, resolveDependency);
            Messenger.Default.Register<DependencyMessage<IConnectionService>>(this, resolveConnectionDependency);
        }

        private void resolveConnectionDependency(DependencyMessage<IConnectionService> obj)
        {
            obj.Resolved(DependencyService.Get<IConnectionService>());
        }

        private async void makeQuestion(QuestionMessage msg)
        {
            bool answer = await this.MainPage.DisplayAlert
                (msg.Title, msg.Message, msg.Caption, msg.CancelCaption);

            if (answer)
            {
                msg.Yes?.Invoke();
            }
            else
            {
                msg.No?.Invoke();
            }
        }

        private void resolveDependency(DependencyMessage<IStorage> obj)
        {
            // La chiamata a Resolved "rimbalza" verso il viewmodel iniettando
            // l'instanza della classe concreta che risolve IStorage
            obj.Resolved(DependencyService.Get<IStorage>());
        }

        private async void showMsg(ShowDialogMessage msg)
        {
            await this.MainPage.DisplayAlert
                (msg.Title, msg.Message, msg.Caption);
        }

        private void openView(OpenViewMessage obj)
        {
            string ns = "SanGiu.Taxi.XF.";

            Type type = Type.GetType(ns + obj.NewPage);

            if (type == null)
            {
                type = Type.GetType("SanGiu.Taxi.XF.Pages." + obj.NewPage);
            }

            if (type != null)
            {
                Page pg = Activator.CreateInstance(type) as Page;

                if (pg != null)
                {
                    // C'è un parametro...
                    if (obj.Parameter != null)
                    {
                        // ... recupero il viewmodel della pagina... (se esiste)
                        ApplicationViewModelBase vm = null;
                        if (pg.Resources.ContainsKey("viewmodel"))
                        {
                            vm = pg.Resources["viewmodel"] as ApplicationViewModelBase;
                            vm.Parameter = obj.Parameter;
                            vm.Init();
                        }
                    }

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await this.MainPage.Navigation.PushModalAsync(pg);
                    });
                }
            }
        }
    }
}