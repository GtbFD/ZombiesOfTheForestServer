using System.Net.Sockets;
using app.interfaces;

namespace app;

public class BroadcastingPacket : ISendMessage
{

    private List<Socket> connectedPlayers;
    
    public BroadcastingPacket(List<Socket> connectedPlayers)
    {
        this.connectedPlayers = connectedPlayers;
    }
    
    public void Send(byte[] data)
    {
        foreach (var connection in connectedPlayers.ToList())
        {
            connection.Send(data);
        }
    }
}