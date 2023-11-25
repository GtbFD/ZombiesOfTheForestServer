using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using app;
using app.handlers;
using app.server;
using app.utils.io;
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
        Console.WriteLine("- {0} player(s) connected", PlayerList.GetInstance().GetList().Count);
        
        var hasPlayers = PlayerList.GetInstance().HasPlayers();
        
        while (true)
        {
            if (hasPlayers)
            {
                
                var buffer = new ServerInfo().GetBuffer();
                var packetBytes = playerConnection.Receive(buffer);
                var packetReceived = Encoding.UTF8.GetString(buffer, 0, packetBytes);
                
                if (packetReceived != "")
                {
                    var packets = new List<IPacketHandler>
                    {
                        new LoginPlayerHandler(playerConnection),
                        new DisconnectPlayerHandler(playerConnection),
                    };

                    var packetManager = new PacketManager(packets);
                    packetManager.Manager(packetReceived);

                    hasPlayers = PlayerList.GetInstance().HasPlayers();
                }
            }
        }

    }

}