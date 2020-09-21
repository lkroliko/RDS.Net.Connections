namespace RDS.Net.Connections.Senders
{
    public interface ISender
    {
        bool WriteLine(string value);
    }
}