﻿using Moq;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionTests
{
    [Trait("Category", "Connection")]
    public class Connected
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();
        Connection _connection;

        public Connected()
        {
            _connection = new Connection(_connectionHandler, _readerFactory, _writerFactory);
        }

        [Fact]
        public void ItExists()
        {
           _connection.Connected += delegate { };
        }

        [Fact]
        public void WhenConnectionHandlerConnectedRaisedThenEventConnectedCalled()
        {
            int calledCount = 0;
            _connection.Connected += (sender, args) => { calledCount++; };

            Mock.Get(_connectionHandler).Raise(c => c.Connected += null, new EventArgs());

            Assert.Equal(1, calledCount);
        }
    }
}
