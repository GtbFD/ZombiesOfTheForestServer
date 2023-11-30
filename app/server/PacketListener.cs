using System.Net.Sockets;
using System.Text;
using app.handlers;
using app.utils.io;

namespace app.server;

class PacketListener
{
    private Task<Socket> playerConnection;

    public PacketListener(Task<Socket> connection)
    {
        playerConnection = connection;
    }

    public void ListenToPackets()
    {
        var hasPlayers = PlayerList.GetInstance().HasPlayers();
        
        while (true)
        {
            if (hasPlayers)
            {
                var buffer = new ServerInfo().GetBuffer();
                var packetBytes = playerConnection.Result.ReceiveAsync(buffer);
                var packetReceived = Encoding.UTF8.GetString(buffer, 0, packetBytes.Result);

                var packet = Encoding.ASCII.GetBytes(packetReceived);

                if (!packetReceived.Equals(""))
                {
                    
                    var packets = new List<IPacketHandler>
                    {
                        new LoginPlayerHandler(playerConnection.Result),
                        new DisconnectPlayerHandler(playerConnection.Result),
                        new PlayerLocalizationHandler(playerConnection.Result)
                    };

                    var packetManager = new PacketManager(packets);
                    packetManager.Manager(packet);

                    hasPlayers = PlayerList.GetInstance().HasPlayers();
                }
            }
        }
    }

}