using System;

namespace RDS.Net.Connections.Wrappers
{
    internal interface IDateTime
    {
        DateTime Now { get; }
    }
}