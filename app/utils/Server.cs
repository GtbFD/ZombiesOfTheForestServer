using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using app;
using app.Packets;
using Newtonsoft.Json;

class Server
{
    private static int connections = 0;
    private string data;
    private byte[] bufferLength;
    private Socket playerConnection;

    public Server(Socket connection)
    {
        bufferLength = new byte[1024];
        playerConnection = connection;
    }

    public Server()
    {
        
    }

    public void Listening()
    {
        Console.WriteLine("- {0} players connected", GetListConnectedPlayers().Count);
        while (RunForever())
        {
            Handler();
        }
        
    }

    private List<Socket> GetListConnectedPlayers()
    {
        return ListPlayers.GetInstance().GetList();
    }

    private bool RunForever()
    {
        return true;
    }

    private void Handler()
    {
        if (HasPlayers())
        {
            var packetBytes = playerConnection.Receive(bufferLength);
            var packetReceived = Encoding.UTF8.GetString(bufferLength, 0, packetBytes);
            
            var packets = new List<IPacketHandler>
            {
                new PlayerListPacket(playerConnection),
                new DisconnectPlayerPacket(playerConnection),
                new PlayerPositionPacket(playerConnection)
            };

            var packetManager = new PacketManager(packets);
            packetManager.Manager(packetReceived);
        }
    }

    private bool HasPlayers()
    {
        return GetListConnectedPlayers().Count >= 1;
    }

}