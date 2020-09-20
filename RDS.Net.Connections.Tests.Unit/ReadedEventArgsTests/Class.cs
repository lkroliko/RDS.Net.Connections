using RDS.Net.Connections.Readers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ReadedEventArgsTests
{
    [Trait("Category", "ReadedEventArgs")]
    public class Class
    {
        [Fact]
        public void ItExists()
        {
            ReadedEventArgs args = new ReadedEventArgs("value");
        }
    }
}
