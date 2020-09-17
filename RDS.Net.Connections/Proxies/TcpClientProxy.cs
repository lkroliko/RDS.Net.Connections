using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using RDS.Net.Connections.Wrappers;

namespace RDS.Net.Connections.Proxies
{
    internal class TcpClientProxy : INetClientProxy
    {
        TcpClient _client;
        string _hostname;
        int _port;
        public bool IsConnected { get { return GetIsConnected(); } }

        public TcpClientProxy(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;
        }

        public void Connect()
        {
            _client = new TcpClient(_hostname, _port);
        }

        private bool GetIsConnected()
        {
            if (_client != null && _client.Connected)
                return true;
            else
                return false;
        }

        public IStreamReader GetStreamReader()
        {
            return new StreamReaderWrapper(_client.GetStream());
        }

        public IStreamWriter GetStreamWriter()
        {
            return new StreamWriterWrapper(_client.GetStream());
        }
    }
}
