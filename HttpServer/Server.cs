using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HttpServer
{
    public class Server
    {
        private TcpListener _server;

        public void Start()
        {
            try
            {
                // Set the TcpListener on port 13000.
                const int port = 13000;
                var localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                _server = new TcpListener(localAddr, port);
                _server.Start();

                // Enter the listening loop.
                var thread = new Thread(StartListenLoop);  
                thread.Start();

            }
            catch(SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                // Stop listening for new clients.
                _server.Stop();
            }
        }
        
        private void StartListenLoop()
        {
            while(true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                var tcpClient = _server.AcceptTcpClient();
                Console.WriteLine("Connected!");
                
                // Get a stream object for reading and writing
                var networkStream = tcpClient.GetStream();

                const string httpBody = "Hello world!";
                var httpHeader =
                    "HTTP/1.0 200 OK\n" +
                    "Content-Type: text/plain; charset=UTF-8\n" +
                    "Content-Length: " + httpBody.Length + "\n\n";
                var httpResponse = httpHeader + httpBody;
                var responseBytes = Encoding.UTF8.GetBytes(httpResponse);
                networkStream.Write(responseBytes, 0, httpResponse.Length);

                Console.WriteLine("Response Sent /> : " + Encoding.UTF8 .GetString(responseBytes, 0, responseBytes.Length));

                tcpClient.Close();
            }
        }
    }
}