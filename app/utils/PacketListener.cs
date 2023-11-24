using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using app;
using app.Packets;
using app.server;
using Newtonsoft.Json;

class PacketListener
{
    private Socket playerConnection;

    public PacketListener(Socket connection)
    {
        playerConnection = connection;
    }

    public void ListenToPackets()
    {
        Console.WriteLine("- {0} players connected", PlayerList.GetInstance().GetList().Count);

        var hasPlayers = PlayerList.GetInstance().HasPlayers();
        
        while (hasPlayers)
        {
            var buffer = new ServerInfo().GetBuffer();
            var packetBytes = playerConnection.Receive(buffer);
            var packetReceived = Encoding.UTF8.GetString(buffer, 0, packetBytes);

            var packets = new List<IPacketHandler>
            {
                new PlayerListPacket(playerConnection),
                new DisconnectPlayerPacket(playerConnection),
                //new PlayerPositionPacket(playerConnection)
            };

            var packetManager = new PacketManager(packets);
            packetManager.Manager(packetReceived);
            
            hasPlayers = PlayerList.GetInstance().HasPlayers();
        }

    }

}