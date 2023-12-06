using System.Net;
using System.Net.Sockets;
using static app.server.ServerInfo;
using app.handlers;
using app.interfaces;
using app.utils.io;

namespace app.server;

public class PacketListenerUdp : IPacketListener
{
    private IPEndPoint ipEndPoint;
    private UdpClient connection;
    
    public void SetConnection(Socket connection)
    {
        var udpClient = new UdpClient();
        udpClient.Client = connection;
        this.connection = udpClient;
    }

    public void SetIpEndPoint(IPEndPoint ipEndPoint)
    {
        this.ipEndPoint = ipEndPoint;
    }

    public void ListenToPackets()
    {
        while (true)
        {
            
            var packetUdpBytes = connection.Receive(ref ipEndPoint);


            var packets = new List<IPacketHandler>
            {
                new PlayerLocalizationHandler(connection),
            };

            var packetManager = new PacketManager(packets);
            packetManager.Manager(packetUdpBytes);
            
        }
    }
}