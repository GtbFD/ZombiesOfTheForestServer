using System.Net;
using System.Net.Sockets;
using System.Text;
using app.handlers;
using app.utils.io;
using static app.server.ServerInfo;

namespace app.server;

class PacketListener
{
    private Socket playerConnectionTCP;
    private UdpClient playerConnectionUDP;

    public void SetConnectionTCP(Socket connectionTCP)
    {
        playerConnectionTCP = connectionTCP;
        ;
    }

    public void SetConnectionUDP(UdpClient connectionUDP)
    {
        playerConnectionUDP = connectionUDP;
    }

    public void ListenToPackets()
    {
        var hasPlayers = PlayerList.GetInstance().HasPlayers();

        while (true)
        {
            if (hasPlayers)
            {
                var buffer = new ServerInfo().GetBuffer();
                var packetBytes = playerConnectionTCP.Receive(buffer);
                var packetReceivedTCP = Encoding.ASCII.GetString(buffer, 0, packetBytes);
                var packetTcpBytes = Encoding.ASCII.GetBytes(packetReceivedTCP);
                

                var packets = new List<IPacketHandler>
                {
                    new LoginPlayerHandler(playerConnectionTCP),
                    new DisconnectPlayerHandler(playerConnectionTCP),
                    
                    new PlayerLocalizationHandler(playerConnectionUDP)
                };

                var packetManager = new PacketManager(packets);
                packetManager.Manager(packetTcpBytes);

                hasPlayers = PlayerList.GetInstance().HasPlayers();
            }
        }
    }
}