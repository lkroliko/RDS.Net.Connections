using Moq;
using RDS.Logging;
using RDS.Net.Connections.Abstractions;
using RDS.Net.Connections.Receivers;
using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ReceiverTests
{
    [Trait("Category", "Receiver")]
    public class Class
    {
        IConnectionHandler _connection = Mock.Of<IConnectionHandler>();
        ILogger _logger = Mock.Of<ILogger>();

        [Fact]
        public void ItExists()
        {
            new Receiver(_connection, _logger);
        }

        [Fact]
        public void ItImplementIReceiver()
        {
            Assert.IsAssignableFrom<IReceiver>(new Receiver(_connection, _logger));
        }
    }
}
