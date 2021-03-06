﻿using RDS.Logging;
using RDS.Net.Connections.Abstractions;
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
        ILogger _logger;

        internal Receiver(IConnectionHandler connection, ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public void Start(CancellationToken token)
        {
            IStreamReader streamReader = GetStreamReader();
            while (token.IsCancellationRequested == false)
            {
                try
                {
                    string value = streamReader.ReadLine();
                    _logger.Trace($"Received: {value}");
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
