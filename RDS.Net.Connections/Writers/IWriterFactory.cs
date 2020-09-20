namespace RDS.Net.Connections.Writers
{
    interface IWriterFactory
    {
        IWriter Get(IConnectionHandler connection);
    }
}