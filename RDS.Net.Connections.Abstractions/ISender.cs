namespace RDS.Net.Connections.Abstractions
{
    public interface ISender
    {
        bool SendLine(string value);
    }
}