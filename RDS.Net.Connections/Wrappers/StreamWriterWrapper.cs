using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace RDS.Net.Connections.Wrappers
{
    internal class StreamWriterWrapper : IStreamWriter
    {
        StreamWriter _writer;

        internal StreamWriterWrapper(NetworkStream networkStream)
        {
            _writer = new StreamWriter(networkStream);
        }

        public void WriteLine(string value)
        {
            _writer.WriteLine(value);
        }

        public void Flush()
        {
            _writer.Flush();
        }
    }
}
