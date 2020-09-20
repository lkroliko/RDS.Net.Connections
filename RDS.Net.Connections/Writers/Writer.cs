﻿using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace RDS.Net.Connections.Writers
{
    class Writer : IWriter
    {
        IConnection _connection;
        IStreamWriter _streamWriter;

        public Writer(IConnection connection)
        {
            _connection = connection;
        }

        public bool WriteLine(string value)
        {
            if (_connection.IsConnected == false)
                _connection.Connect();
            if (_streamWriter == null)
                _streamWriter = _connection.GetStreamWriter();
            try
            {
                _streamWriter.WriteLine(value);
                _streamWriter.Flush();
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
