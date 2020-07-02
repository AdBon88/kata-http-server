using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class MyTcpListener
{
    public static void Main()
    {
        TcpListener server=null;
        try
        {
            // Set the TcpListener on port 13000.
            const int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while(true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                var tcpClient = server.AcceptTcpClient();
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
                
                //data = null;
                //int i;
    
                // Loop to receive all the data sent by the client.
                // while((i = stream.Read(bytes, 0, bytes.Length))!=0)
                // {
                //     // Translate data bytes to a ASCII string.
                //     data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                //     Console.WriteLine("Received: {0}", data);
                //
                //     // Process the data sent by the client.
                //     data = data.ToUpper();
                //
                //     byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                //
                //     // Send back a response.
                //     stream.Write(msg, 0, msg.Length);
                //     Console.WriteLine("Sent: {0}", data);
                // }

                // Shutdown and end connection
                tcpClient.Close();
            }
        }
        catch(SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            // Stop listening for new clients.
            server.Stop();
        }

        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
    }
}