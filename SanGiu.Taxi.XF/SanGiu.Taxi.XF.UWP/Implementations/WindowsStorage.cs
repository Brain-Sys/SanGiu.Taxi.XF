using SanGiu.Taxi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SanGiu.Taxi.XF.UWP.Implementations.WindowsStorage))]
namespace SanGiu.Taxi.XF.UWP.Implementations
{
    public class WindowsStorage : IStorage
    {
        public string GetCurrentDirectory()
        {
            return ApplicationData.Current.RoamingFolder.Path;
        }

        public string GetDbDirectory()
        {
            return ApplicationData.Current.RoamingFolder.Path;
        }
    }
}