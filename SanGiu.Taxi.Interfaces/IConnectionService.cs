using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanGiu.Taxi.Interfaces
{
    public interface IConnectionService
    {
        event EventHandler<object> SomethingHappened;

        int BytesRead { get; set; }

        Task StartAsync();
        Task StopAsync();
        Task WriteAsync(byte value);
    }
}