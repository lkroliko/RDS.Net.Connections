namespace RDS.Net.Connections.Receivers
{
    interface IReceiverFactory
    {
        IReceiver Get(IConnectionHandler connection);
    }
}