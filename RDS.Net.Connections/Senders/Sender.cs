using RDS.Logging;
using RDS.Net.Connections.Abstractions;
using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace RDS.Net.Connections.Senders
{
    class Sender : ISender
    {
        IConnectionHandler _connection;
        IStreamWriter _streamWriter;
        ILogger _logger;

        public Sender(IConnectionHandler connection, ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public bool SendLine(string value)
        {
            if (_connection.IsConnected == false)
                _connection.Connect();
            if (_streamWriter == null)
                _streamWriter = _connection.GetStreamWriter();
            try
            {
                _streamWriter.WriteLine(value);
                _streamWriter.Flush();
                _logger.Trace($"Sended: {value}");
                return true;
            }
            catch
            {
                _streamWriter = null;
                return false;
            }
        }
    }
}
