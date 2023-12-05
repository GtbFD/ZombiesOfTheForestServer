using System.Net.Sockets;
using app.interfaces;
using app.models;
using app.utils.io;

namespace app;

public class BroadcastingPacket : ISendMessage
{

    private List<Player> connectedPlayers;
    private Socket connection;
    
    public BroadcastingPacket(Socket connection, List<Player> connectedPlayers)
    {
        this.connectedPlayers = connectedPlayers;
        this.connection = connection;
    }
    
    public void Send(byte[] data)
    {
        foreach (var player in connectedPlayers.ToList())
        {
            if (player.tcpConnection.Connected && !player.tcpConnection.RemoteEndPoint.Equals(connection))
            {
                player.tcpConnection.Send(data);
            }
        }
    }
    
    public void SendAll(byte[] data)
    {
        foreach (var player in connectedPlayers.ToList())
        {
            if (player.tcpConnection.Connected)
            {
                player.tcpConnection.Send(data);
            }
        }
    }
}