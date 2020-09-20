using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections
{
    class ConnectionManager : IConnectionManager
    {
        public IConnection Connection { get; private set; }
        public IReader Reader { get { return GetReader(); } }
        public IWriter Writer { get { return GetWriter(); } }

        IReader _reader;
        IReaderFactory _readerFactory;
        IWriter _writer;
        IWriterFactory _writerFactory;

        internal ConnectionManager(IConnection connection, IReaderFactory readerFactory, IWriterFactory writerFactory)
        {
            Connection = connection;
            _readerFactory = readerFactory;
            _writerFactory = writerFactory;
        }

        private IReader GetReader()
        {
            if (_reader == null)
                _reader = _readerFactory.Get(Connection);
            return _reader;
        }

        private IWriter GetWriter()
        {
            if (_writer == null)
                _writer = _writerFactory.Get(Connection);
            return _writer;
        }
    }
}
