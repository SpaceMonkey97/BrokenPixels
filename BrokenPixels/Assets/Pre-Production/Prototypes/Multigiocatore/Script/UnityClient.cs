using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

[Obsolete]
public class UnityClient : MonoBehaviour
{
    public static UnityClient instance;
    public static int dataBufferSize = 4096;

    public string ip = IPAddress.Loopback.ToString();
    public int port = 130; //CHECK port on server script
    public int myId = 1;
    public TCP tcp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
    }
    
    public void ConnectedToServer()
    {
        tcp.Connect();
    }

    public class TCP
    {
        public TcpClient socket;

        private NetworkStream stream;
        private byte[] receiveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, ConnectCollback, socket);
        }

        private void ConnectCollback(IAsyncResult result)
        {
            socket.EndConnect(result);

            if (!socket.Connected) return;

            stream = socket.GetStream();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReciveCallback, null);
        }

        private void ReciveCallback(IAsyncResult result)
        {
            try
            {
                int byteLenght = stream.EndRead(result);
                if (byteLenght <= 0)
                {
                    //TODO disconect
                }

                byte[] data = new byte[byteLenght];
                Array.Copy(receiveBuffer, data, byteLenght);

                //TODO Handle data
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReciveCallback, null);
            }
            catch (Exception ex)
            {
                Debug.Log($"Error receiving TCP data {ex}");
                //TODO disconect
            }
        }
    }
}
