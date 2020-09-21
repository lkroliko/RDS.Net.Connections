using RDS.Net.Connections.Receivers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ReadedEventArgsTests
{
    [Trait("Category", "ReceivedEventArgs")]
    public class Class
    {
        [Fact]
        public void ItExists()
        {
            ReceivedEventArgs args = new ReceivedEventArgs("value");
        }
    }
}
