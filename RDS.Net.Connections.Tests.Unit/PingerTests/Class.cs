using Moq;
using RDS.Net.Connections.Pingers;
using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.PingerTests
{
    [Trait("Category", "Pinger")]
    public class Class
    {
        ITask _task = Mock.Of<ITask>();
        IThread _thread = Mock.Of<IThread>();
        string _value = "ping";
        int _milisecondsIntervalTime = 1;

        [Fact]
        public void ItExists()
        {
            new Pinger(_task, _thread, _value, _milisecondsIntervalTime);
        }
    }
}
