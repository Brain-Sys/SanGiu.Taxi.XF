using SanGiu.Taxi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SanGiu.Taxi.XF
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

#if DEBUG
            this.txtUsername.Text = "corso";
            this.txtPassword.Text = "macerata";
#endif
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var lr = Authentication.Check(this.txtUsername.Text, this.txtPassword.Text);

            if (lr.Success == true)
            {
                // Pagina successiva
                MenuPage pg = new MenuPage(lr);
                await this.Navigation.PushAsync(pg);
                // await this.Navigation.PushModalAsync(pg);
            }
            else
            {
                // Messaggio
                await this.DisplayAlert("Errore", "Login fallito!", "Chiudi");
            }
        }
    }
}