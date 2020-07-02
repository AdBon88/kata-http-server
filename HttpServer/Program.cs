using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using HttpServer;

class MyTcpListener
{
    private static TcpListener _server;
    public static void Main()
    {
        var server = new Server();
        server.Start();
    }
}