using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDS.Net.Connections.Receivers
{
    class Receiver : IReceiver
    {
        public event EventHandler<ReceivedEventArgs> Received = delegate { };
        internal virtual void OnReaded(ReceivedEventArgs args) { Received.Invoke(this, args); }
        IConnectionHandler _connection;

        internal Receiver(IConnectionHandler connection)
        {
            _connection = connection;
        }

        public void Start(CancellationToken token)
        {
            IStreamReader streamReader = GetStreamReader();
            while (token.IsCancellationRequested == false)
            {
                try
                {
                    string value = streamReader.ReadLine();
                    ReceivedEventArgs args = new ReceivedEventArgs(value);
                    OnReaded(args);
                }
                catch
                {
                    streamReader = GetStreamReader();
                }
            }
        }

        private IStreamReader GetStreamReader()
        {
            if (_connection.IsConnected == false)
                _connection.Connect();
            return _connection.GetStreamReader();
        }
    }
}
