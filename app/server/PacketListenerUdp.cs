using System.Net;
using System.Net.Sockets;
using static app.server.ServerInfo;
using app.handlers;
using app.utils.io;

namespace app.server;

public class PacketListenerUdp
{
    private IPEndPoint endPoint;
    private UdpClient playerConnectionUdp;

    public void SetConnectionUDP(UdpClient connectionUDP, IPEndPoint endPoint)
    {
        playerConnectionUdp = connectionUDP;
        this.endPoint = endPoint;
    }

    public void ListenToPackets()
    {
        while (true)
        {
            
            var packetUdpBytes = playerConnectionUdp.Receive(ref endPoint);


            var packets = new List<IPacketHandler>
            {
                new PlayerLocalizationHandler(playerConnectionUdp),
            };

            var packetManager = new PacketManager(packets);
            packetManager.Manager(packetUdpBytes);
            
        }
    }
}