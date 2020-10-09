using Moq;
using RDS.Logging;
using RDS.Net.Connections.Receivers;
using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ReaderTests
{
    [Trait("Category", "Receiver")]
    public class Received
    {
        IConnectionHandler _connection = Mock.Of<IConnectionHandler>();
        IStreamReader _streamReader = Mock.Of<IStreamReader>();
        ILogger _logger = Mock.Of<ILogger>();
        Receiver _receiver;
        CancellationTokenSource _cancellationSource = new CancellationTokenSource();

        public Received()
        {
            Mock.Get(_connection).Setup(c => c.GetStreamReader()).Returns(_streamReader);
            Mock.Get(_connection).Setup(c => c.IsConnected).Returns(true);
            _receiver = new Receiver(_connection, _logger);
        }

        [Fact]
        public void ItExists()
        {
            _receiver.Received += delegate { };
        }

        [Fact]
        public void StreamReaderGivenValueThenValueReturned()
        {
            string expected = "value";
            string result = string.Empty;
            Mock.Get(_streamReader).Setup(s => s.ReadLine()).Callback(() =>
            {
                _cancellationSource.Cancel();
            }).Returns(expected);
            _receiver.Received += (sender, args) => { result = args.Value; };

            _receiver.Start(_cancellationSource.Token);

            Assert.Same(expected, result);
        }

        [Fact]
        public void StreamReaderGivenValuesThenReaderReadLineCalled()
        {
            int readCount = 0;
            Mock.Get(_streamReader).Setup(s => s.ReadLine()).Callback(() =>
            {
                readCount++;
                if (readCount == 3)
                    _cancellationSource.Cancel();
            }).Returns("value");

            _receiver.Start(_cancellationSource.Token);

            Mock.Get(_streamReader).Verify(s => s.ReadLine(), Times.Exactly(3));
        }

        [Fact]
        public void StreamReaderGivenValuesThenLoggerCalled()
        {
            int readCount = 0;
            Mock.Get(_streamReader).Setup(s => s.ReadLine()).Callback(() =>
            {
                readCount++;
                if (readCount == 3)
                    _cancellationSource.Cancel();
            }).Returns("value");

            _receiver.Start(_cancellationSource.Token);

            Mock.Get(_logger).Verify(l => l.Trace("Received: value"), Times.Exactly(3));
        }
    }
}
