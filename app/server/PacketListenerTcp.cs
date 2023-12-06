using System.Net.Sockets;
using System.Text;
using app.handlers;
using app.interfaces;
using app.utils.io;


namespace app.server;

class PacketListenerTcp : IPacketListener
{
    private Socket playerConnection;
    
    public void SetConnection(Socket connection)
    {
        playerConnection = connection;
    }

    public void ListenToPackets()
    {
        var hasPlayers = PlayerList.GetInstance().HasPlayers();

        while (true)
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