using System.Net.Sockets;
using app.interfaces;
using app.utils.io;

namespace app;

public class BroadcastingPacket : ISendMessage
{

    private List<Socket> connectedPlayers;
    
    public BroadcastingPacket(List<Socket> connectedPlayers)
    {
        this.connectedPlayers = connectedPlayers;
    }
    
    public void Send(IPacket data)
    {
        foreach (var connection in connectedPlayers.ToList())
        {
            connection.Send(SerializePacket.Serialize(data));
        }
    }
}