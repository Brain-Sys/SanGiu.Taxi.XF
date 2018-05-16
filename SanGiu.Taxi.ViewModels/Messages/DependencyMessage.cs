using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.ViewModels.Messages
{
    public class DependencyMessage<T> where T : class
    {
        // Callback
        public Action<T> Resolved { get; set; }
    }
}