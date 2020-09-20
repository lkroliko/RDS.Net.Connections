﻿namespace RDS.Net.Connections.Readers
{
    interface IReaderFactory
    {
        IReader Get(IConnectionHandler connection);
    }
}