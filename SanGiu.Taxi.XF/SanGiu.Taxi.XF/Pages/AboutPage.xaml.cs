using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SanGiu.Taxi.XF.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();
		}

        private void btnChangeTheme_Clicked(object sender, EventArgs e)
        {
            var mb = (Color)Application.Current.Resources["MainBrand"];
            mb = Color.Red;

            Application.Current.Resources["MainBrand"] = mb;
        }
    }
}