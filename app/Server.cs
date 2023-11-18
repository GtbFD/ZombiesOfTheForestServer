using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

class Server
{
    private static int connections = 0;
    private string data;
    private byte[] bufferLength;
    private Socket playerConnection;

    private List<Socket> connectedPlayers;

    private DisconnectPlayerPacket disconnectPlayerPacket;

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
        int packetBytes = playerConnection.Receive(bufferLength);
        string packet = Encoding.UTF8.GetString(bufferLength, 0, packetBytes);
        
        disconnectPlayerPacket = new DisconnectPlayerPacket(playerConnection, connectedPlayers);
        disconnectPlayerPacket.Handler(packet);
    }
    private void SendMessageToClient(string packetData)
    {
        var packet = Encoding.ASCII.GetBytes(packetData);
        playerConnection.Send(packet);
    }

    

    

    private void SendToAllPlayers(byte[] Data)
    {
        foreach (Socket Connection in connectedPlayers.ToList())
        {
            Connection.Send(Data);
        }
    }
}