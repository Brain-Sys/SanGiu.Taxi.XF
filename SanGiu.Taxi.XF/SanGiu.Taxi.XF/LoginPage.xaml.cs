using SanGiu.Taxi.ViewModels;
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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
		}

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            var vm = this.Resources["viewmodel"] as LoginViewModel;
            vm.CheckLogin();
        }
    }
}