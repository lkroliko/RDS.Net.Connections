using Moq;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ReaderTests
{
    [Trait("Category", "Reader")]
    public class Readed
    {
        IConnection _connection = Mock.Of<IConnection>();
        IStreamReader _streamReader = Mock.Of<IStreamReader>();
        Reader _reader;
        CancellationTokenSource _cancellationSource = new CancellationTokenSource();

        public Readed()
        {
            Mock.Get(_connection).Setup(c => c.GetStreamReader()).Returns(_streamReader);
            Mock.Get(_connection).Setup(c => c.IsConnected).Returns(true);
            _reader = new Reader(_connection);
        }

        [Fact]
        public void ItExists()
        {
            _reader.Readed += delegate { };
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
            _reader.Readed += (sender, args) => { result = args.Value; };

            _reader.Start(_cancellationSource.Token);

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

            _reader.Start(_cancellationSource.Token);

            Mock.Get(_streamReader).Verify(s => s.ReadLine(), Times.Exactly(3));
        }
    }
}
