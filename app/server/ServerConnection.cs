using System.Net;

namespace app.server;

public class ServerConnection
{
    public void Config(string type)
    {
        if (type.Equals("tcp"))
        {
            
            new Thread(() => Tcp()).Start();
        }

        if (type.Equals("udp"))
        {
            new Thread(() => Udp()).Start();
        }
    }

    private void Tcp()
    {
        Console.WriteLine($"TCP Started");
        var connection = new TCPServer().Config();
        
        while (true)
        {

            var connectionListener = connection.Accept();

            var packetListener = new PacketListener();
            packetListener.ConfigListener("tcp", connectionListener);
        }
    }

    private void Udp()
    {
        Console.WriteLine($"UDP Started");
        
        var udpServer = new UDPServer();

        var socketUdp = udpServer.Config();

        var endPoint = new IPEndPoint(IPEndPoint.Parse(ServerInfo.GetInstance().GetHostUDP()).Address,
            ServerInfo.GetInstance().GetPortUDP());
        
        var packetListener = new PacketListener();
        packetListener.SetIpEndPoint(endPoint);
        packetListener.ConfigListener("udp", socketUdp.Client);

    }
}