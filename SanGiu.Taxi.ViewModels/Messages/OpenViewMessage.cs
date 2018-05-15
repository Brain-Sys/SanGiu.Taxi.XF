using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.ViewModels.Messages
{
    public class OpenViewMessage
    {
        public string NewPage { get; set; }
        public bool Modal { get; set; }
        public object Parameter { get; set; }
    }
}