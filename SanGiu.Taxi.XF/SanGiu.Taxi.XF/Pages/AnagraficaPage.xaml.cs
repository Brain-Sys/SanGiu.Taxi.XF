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
	public partial class AnagraficaPage : ContentPage
	{
		public AnagraficaPage ()
		{
            //var rd = Application.Current.Resources;
            //var s = rd["Xamarin.Forms.Button"];

            InitializeComponent ();
		}
	}
}