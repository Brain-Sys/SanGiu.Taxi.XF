using SanGiu.Taxi.XF.Styles;
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
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            var minutes = DateTime.Now.Minute;
            bool even = minutes % 2 == 0;

            if (even)
            {
                // Application.Current.Resources;
                this.Resources.Add(new DefaultStylesEven());
            }
            else
            {
                this.Resources.Add(new DefaultStyles());
            }

            InitializeComponent();
        }
    }
}