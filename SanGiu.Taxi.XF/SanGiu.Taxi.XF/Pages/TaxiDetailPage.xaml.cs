using SanGiu.Taxi.ViewModels;
using SanGiu.Taxi.ViewModels.VM;
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
    public partial class TaxiDetailPage : TabbedPage
    {
        public TaxiDetailPage()
        {
            InitializeComponent();
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var vm = this.Resources["viewmodel"] as TaxiDetailViewModel;
            var x = Math.Abs(e.TotalX);
            var y = Math.Abs(e.TotalY);

            // if (e.StatusType != GestureStatus.Completed) return;
            if (x == 0 && y == 0) return;

            if (x > y)
            {
                // SX / DX
                if (e.TotalX > 0)
                {
                    // DX
                    vm.PrintCommand.Execute(null);
                }
                else
                {
                    // SX
                    vm.DownloadCommand.Execute(null);
                }
            }
            else
            {
                // UP / DOWN
            }
        }
    }
}