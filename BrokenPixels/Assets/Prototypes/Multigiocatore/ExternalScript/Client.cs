using System;
using System.Net;
using System.Net.Sockets;

namespace PrototipoSocket
{
    class Client
    {
        public static int dataBufferSize = 4096;
        public int id;
        public TCP tcp;

        public class TCP {

            public TcpClient socket;
            private readonly int id;
            private NetworkStream stream;
            private byte[] receiveBuffer;

            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _tcpSocket)
            {
                socket = _tcpSocket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReciveCallback, null);
               
                //TODO send Welcome packet
            }

            private void ReciveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLenght = stream.EndRead(_result);
                    if (_byteLenght <= 0)
                    {
                        //TODO disconect
                    }

                    byte[] _data = new byte[_byteLenght];
                    Array.Copy(receiveBuffer, _data, _byteLenght);

                    //TODO Handle data
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReciveCallback, null);   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reciving TCP data {ex}");
                    //TODO disconect
                }
            }
        }


        public Client(int _clientId)
        {
            id = _clientId;
            tcp = new TCP(id);
        }
    }
}
