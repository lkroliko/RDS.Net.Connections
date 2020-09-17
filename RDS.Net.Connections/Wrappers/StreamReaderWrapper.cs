using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace RDS.Net.Connections.Wrappers
{
    internal class StreamReaderWrapper : IStreamReader
    {
        StreamReader _reader;

        internal StreamReaderWrapper(NetworkStream networkStream)
        {
            _reader = new StreamReader(networkStream);
        }

        public string ReadLine()
        {
            return _reader.ReadLine();
        }
    }
}
