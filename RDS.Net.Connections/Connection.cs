using Microsoft.Extensions.Logging;
using RDS.Net.Connections.Abstractions;
using RDS.Net.Connections.Receivers;
using RDS.Net.Connections.Senders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDS.Net.Connections
{
    class Connection : IConnection
    {
        public bool IsConnected => _connectionHandler.IsConnected;
        public IReceiver Receiver { get { return GetReceiver(); } }
        public ISender Sender { get { return GetSender(); } }
        public event EventHandler<EventArgs> Connected { add { _connectionHandler.Connected += value; } remove { _connectionHandler.Connected -= value; } }

        public event EventHandler<ConnectionStartedEventArgs> Started = delegate { };
        internal virtual void OnStarted(ConnectionStartedEventArgs args) { Started.Invoke(this, args); } 

        IConnectionHandler _connectionHandler;
        IReceiver _receiver;
        IReceiverFactory _receiverFactory;
        ISender _sender;
        ISenderFactory _senderFactory;

        internal Connection(IConnectionHandler connectionHandler, IReceiverFactory receiverFactory, ISenderFactory senderFactory)
        {
           _connectionHandler = connectionHandler;
            _receiverFactory = receiverFactory;
            _senderFactory = senderFactory;
        }

        public void Start(CancellationToken token)
        {
            OnStarted(new ConnectionStartedEventArgs(token));
            Receiver?.Start(token);
        }

        private IReceiver GetReceiver()
        {
            if (_receiver == null)
                _receiver = _receiverFactory.Get(_connectionHandler);
            return _receiver;
        }

        private ISender GetSender()
        {
            if (_sender == null)
                _sender = _senderFactory.Get(_connectionHandler);
            return _sender;
        }
    }
}
