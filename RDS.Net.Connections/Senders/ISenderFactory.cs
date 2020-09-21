namespace RDS.Net.Connections.Senders
{
    interface ISenderFactory
    {
        ISender Get(IConnectionHandler connection);
    }
}