using GalaSoft.MvvmLight.Messaging;
using SanGiu.Taxi.ViewModels.Messages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SanGiu.Taxi.XF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // this.MainPage = new NavigationPage(new MainPage());
            this.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            configureMessages();
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
            Messenger.Default.Register<OpenViewMessage>(this, openView);
        }

        private async void openView(OpenViewMessage obj)
        {
            Type type = Type.GetType("SanGiu.Taxi.XF." + obj.NewPage);

            if (type != null)
            {
                Page pg = null;

                if (obj.Parameter == null)
                {
                    pg = Activator.CreateInstance(type) as Page;
                }
                else
                {
                    pg = Activator.CreateInstance(type, obj.Parameter) as Page;
                }

                if (pg != null)
                {
                    await this.MainPage.Navigation.PushModalAsync(pg);
                }
            }
        }

        private async void showMsg(ShowDialogMessage msg)
        {
            await this.MainPage.DisplayAlert
                (msg.Title, msg.Message, msg.Caption);
        }
    }
}
