using System.Net;
using System.Net.Sockets;
using static app.server.ServerInfo;

namespace app.server;

public class UDPServer
{

    public UdpClient Config()
    {
        var addressFamily = ServerInfoInstance().AddressFamily(ServerInfoInstance().GetHostUDP());
        var port = ServerInfoInstance().GetPortUDP();

        var ipEndPoint = new IPEndPoint(IPAddress.Any, 
            ServerInfoInstance().GetPortUDP());
        
        /*var socketUdp = new Socket(addressFamily, SocketType.Dgram, ProtocolType.Udp);
        socketUdp.Bind(ipEndPoint);*/

        var udpClient = new UdpClient(ipEndPoint);

        return udpClient;
    }

    public EndPoint EndPoint(string host, int port)
    {
        return new IPEndPoint(IPAddress.Parse(host), port);
    }
}