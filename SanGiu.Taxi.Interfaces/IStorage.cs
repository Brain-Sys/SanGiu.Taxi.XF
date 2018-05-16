using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.Interfaces
{
    public interface IStorage
    {
        string GetCurrentDirectory();
        string GetDbDirectory();
    }
}