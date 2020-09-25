using RDS.Net.Connections.Abstractions;

namespace RDS.Net.Connections.Receivers
{
    interface IReceiverFactory
    {
        IReceiver Get(IConnectionHandler connection);
    }
}