using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.ViewModels.Messages
{
    public class ShowDialogMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Caption { get; set; } = "Chiudi";
    }
}