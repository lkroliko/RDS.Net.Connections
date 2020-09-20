using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Readers
{
    public class ReadedEventArgs : EventArgs
    {
        public string Value { get; }

        public ReadedEventArgs(string value)
        {
            Value = value;
        }
    }
}
