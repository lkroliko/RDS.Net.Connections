using Moq;
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

        [Fact]
        public void ItExists()
        {
            new Receiver(_connection);
        }
    }
}
