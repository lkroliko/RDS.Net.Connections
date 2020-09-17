using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Wrappers
{
    internal class DateTimeWrapper : IDateTime
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
