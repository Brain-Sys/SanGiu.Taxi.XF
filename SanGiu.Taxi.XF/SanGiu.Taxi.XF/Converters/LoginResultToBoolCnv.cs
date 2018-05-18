using SanGiu.Taxi.Auth;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SanGiu.Taxi.XF.Converters
{
    class LoginResultToBoolCnv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LoginResult lr = value as LoginResult;

            // SuperUser
            // Admin

            if (lr != null)
            {
                if (lr.Role == "Admin") return true;
                else return false;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}