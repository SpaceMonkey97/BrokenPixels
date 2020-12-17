using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace PrototipoSocket
{
    class Server
    {
        public static int MaxPlayer { get; private set; }

        public static int Port { get; private set; }

        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

        private static TcpListener tcpListener;

        public static void Start(int _maxPlayers, int _port)
        {
            MaxPlayer = _maxPlayers;
            Port = _port;

            Console.WriteLine("Starting server...");
            IntizializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);

            Console.WriteLine($"Server Started on {Port} ");
        }

        public static void TCPConnectionCallback(IAsyncResult _result)
        {
            TcpClient _tcpClient = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);
            Console.WriteLine($"Incoming connection from {_tcpClient.Client.RemoteEndPoint}...");

            for (int i = 1; i <= MaxPlayer; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(_tcpClient);
                    return;
                }
            }

            Console.WriteLine($"{_tcpClient.Client.RemoteEndPoint} failed to connect: Server full!");
        }

        private static void IntizializeServerData()
        {
            for (int i = 1; i <= MaxPlayer; i++)
            {
                clients.Add(i, new Client(i));
            }
        }
    }

}
