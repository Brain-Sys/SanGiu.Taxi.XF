using SanGiu.Taxi.Translations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms.Xaml;

namespace SanGiu.Taxi.XF.Markup
{
    class Translate : IMarkupExtension
    {
        public string Language { get; set; }
        public string Key { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var manager = Vocabulary.ResourceManager;
            var c = CultureInfo.CreateSpecificCulture(this.Language);
            string result = manager.GetString(this.Key, c);

            return result;
        }
    }
}