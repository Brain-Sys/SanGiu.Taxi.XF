﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanGiu.Taxi.SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SynchronousSocketListener.StartListening();
        }
    }
}
