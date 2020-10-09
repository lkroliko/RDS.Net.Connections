using Moq;
using RDS.Logging;
using RDS.Net.Connections.Abstractions;
using RDS.Net.Connections.Senders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.SenderTests
{
    [Trait("Category", "Sender")]
    public class Class
    {
        IConnectionHandler _connection = Mock.Of<IConnectionHandler>();
        ILogger _logger = Mock.Of<ILogger>();

        [Fact]
        public void ItExists()
        {
            new Sender(_connection, _logger);
        }

        [Fact]
        public void ItImoplementsIWriter()
        {
           Assert.IsAssignableFrom<ISender>(new Sender(_connection, _logger));
        }
    }
}
