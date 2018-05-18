using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SanGiu.Taxi.SocketServer
{
    public class SynchronousSocketListener
    {

        // Incoming data from the client.  
        public static string data = null;

        public static void StartListening()
        {
            Debug.WriteLine("Start Listening...");

            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[3];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            Debug.WriteLine(ipAddress.ToString());

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                byte[] msg;
                Socket handler = null;

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    Console.WriteLine("Receveied" + bytesRec);

                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //if (data.IndexOf("<EOF>") > -1)
                    //{
                    //    break;
                    //}

                    // Show the data on the console.  
                    Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.  
                    msg = Encoding.ASCII.GetBytes(data);
                    System.Threading.Thread.Sleep(10000);
                    handler.Send(msg);
                }
                
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
    }
}