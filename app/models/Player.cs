using System.Net.Sockets;

namespace app.models;

public class Player
{
    public int id { get; set; }
    public Socket tcpConnection { get; set; }
    
    public UdpClient udpConnection { get; set; }
    public Localization localization { get; set; }
}