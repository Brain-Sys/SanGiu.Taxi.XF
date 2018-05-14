using SanGiu.Taxi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SanGiu.Taxi.XF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : CarouselPage
    {
        public LoginResult LoginInfo { get; private set; }

        public MenuPage(LoginResult loginResult)
        {
            InitializeComponent();
            this.LoginInfo = loginResult;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.LoginInfo.Role != "Admin")
            {
                //this.page3.IsVisible = false;
                //this.page4.IsVisible = false;
                //this.Children.Remove(this.page3);
                //this.Children.Remove(this.page4);

                this.Children.RemoveAt(2);
                this.Children.RemoveAt(2);
            }
        }
    }
}