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

    private List<Socket> connectedPlayers;
    
    public Server(Socket connection, List<Socket> connectedPlayers)
    {
        bufferLength = new byte[1024];
        this.connectedPlayers = connectedPlayers;
        playerConnection = connection;
    }

    public Server()
    {
        
    }

    public void Listening()
    {
        Console.WriteLine("- {0} players connected", connectedPlayers.Count);
        while (RunForever())
        {
            Handler();
        }
        
    }

    private bool RunForever()
    {
        return true;
    }

    private void Handler()
    {
        var packetBytes = playerConnection.Receive(bufferLength);
        var packetReceived = Encoding.UTF8.GetString(bufferLength, 0, packetBytes);

        if (connectedPlayers.Count < 1) return;
        
        Console.WriteLine(packetReceived);

        var packets = new List<IPacket>
        {
            new PlayerListPacket(playerConnection, connectedPlayers),
            new DisconnectPlayerPacket(playerConnection, connectedPlayers)
        };

        var packetManager = new PacketManager(packets);
        packetManager.Manager(packetReceived);
    }

}