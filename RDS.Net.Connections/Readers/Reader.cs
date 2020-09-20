using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDS.Net.Connections.Readers
{
    class Reader : IReader
    {
        public event EventHandler<ReadedEventArgs> Readed = delegate { };
        internal virtual void OnReaded(ReadedEventArgs args) { Readed.Invoke(this, args); }
        IConnection _connection;

        internal Reader(IConnection connection)
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
                    ReadedEventArgs args = new ReadedEventArgs(value);
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
