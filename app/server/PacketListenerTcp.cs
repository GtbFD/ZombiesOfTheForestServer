using System.Net;
using System.Net.Sockets;
using System.Text;
using app.handlers;
using app.utils.io;
using static app.server.ServerInfo;

namespace app.server;

class PacketListenerTcp
{
    private Socket playerConnection;

    public void SetConnectionTCP(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }
    
    public void ListenToPackets()
    {
        var hasPlayers = PlayerList.GetInstance().HasPlayers();

        while (true)
        {
            if (hasPlayers)
            {
                var buffer = new ServerInfo().GetBuffer();
                var packetBytes = playerConnection.Receive(buffer);
                var packetReceivedTCP = Encoding.ASCII.GetString(buffer, 0, packetBytes);
                var packetTcpBytes = Encoding.ASCII.GetBytes(packetReceivedTCP);

                if (packetBytes != 0)
                {

                    var packets = new List<IPacketHandler>
                    {
                        new LoginPlayerHandler(playerConnection),
                        new DisconnectPlayerHandler(playerConnection),
                    };

                    var packetManager = new PacketManager(packets);
                    packetManager.Manager(packetTcpBytes);

                    hasPlayers = PlayerList.GetInstance().HasPlayers();
                }
            }
        }
    }
}