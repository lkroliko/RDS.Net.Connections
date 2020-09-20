using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDS.Net.Connections
{
    class Connection : IConnection
    {
        public bool IsConnected => _connectionHandler.IsConnected;
        public IReader Reader { get { return GetReader(); } }
        public IWriter Writer { get { return GetWriter(); } }
        public event EventHandler<EventArgs> Connected { add { _connectionHandler.Connected += value; } remove { _connectionHandler.Connected -= value; } }

        public event EventHandler<ConnectionStartedEventArgs> Started = delegate { };
        internal virtual void OnStarted(ConnectionStartedEventArgs args) { Started.Invoke(this, args); } 

        IConnectionHandler _connectionHandler;
        IReader _reader;
        IReaderFactory _readerFactory;
        IWriter _writer;
        IWriterFactory _writerFactory;

        internal Connection(IConnectionHandler connectionHandler, IReaderFactory readerFactory, IWriterFactory writerFactory)
        {
           _connectionHandler = connectionHandler;
            _readerFactory = readerFactory;
            _writerFactory = writerFactory;
        }

        public void Start(CancellationToken token)
        {
            OnStarted(new ConnectionStartedEventArgs(token));
            Reader?.Start(token);
        }

        private IReader GetReader()
        {
            if (_reader == null)
                _reader = _readerFactory.Get(_connectionHandler);
            return _reader;
        }

        private IWriter GetWriter()
        {
            if (_writer == null)
                _writer = _writerFactory.Get(_connectionHandler);
            return _writer;
        }
    }
}
