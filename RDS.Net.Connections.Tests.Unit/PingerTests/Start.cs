using Moq;
using RDS.Net.Connections.Pingers;
using RDS.Net.Connections.Wrappers;
using RDS.Net.Connections.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.PingerTests
{
    [Trait("Category", "Pinger")]
    public class Start
    {
        IConnection _connection = Mock.Of<IConnection>();
        IWriter _writer = Mock.Of<IWriter>();
        ITask _task = Mock.Of<ITask>();
        IThread _thread = Mock.Of<IThread>();
        string _value = "ping";
        int _milisecondsIntervalTime = 1;
        Pinger _pinger;
        CancellationTokenSource _tokenSource = new CancellationTokenSource();
        ConnectionStartedEventArgs _args;

        public Start()
        {
            Mock.Get(_connection).Setup(m => m.Writer).Returns(_writer);
            Mock.Get(_task).Setup(t => t.Run(It.IsAny<Action>())).Callback<Action>((action) => action.Invoke());
            _args = new ConnectionStartedEventArgs(_tokenSource.Token);
            _pinger = new Pinger(_task, _thread, _value, _milisecondsIntervalTime);
        }

        [Fact]
        public void ItExists()
        {
            _tokenSource.Cancel();

            _pinger.Start(_connection, _args);
        }

        [Fact]
        public void WhenCalledThenWriterWriteLineCalled()
        {
            Mock.Get(_writer).Setup(w => w.WriteLine(_value)).Callback(() => _tokenSource.Cancel());

            _pinger.Start(_connection, _args);

            Mock.Get(_writer).Verify(w => w.WriteLine(_value), Times.Once);
        }

        [Fact]
        public void WhenCalledThenThreadSleepCalled()
        {
            Mock.Get(_writer).Setup(w => w.WriteLine(_value)).Callback(() => _tokenSource.Cancel());

            _pinger.Start(_connection, _args);

            Mock.Get(_thread).Verify(t => t.Sleep(_milisecondsIntervalTime), Times.Once);
        }

        [Fact]
        public void WhenCalledThenTaskRunNotCalled()
        {
            _pinger.Start(new object(), _args);

            Mock.Get(_task).Verify(t => t.Run(It.IsAny<Action>()), Times.Never);
        }
    }
}
