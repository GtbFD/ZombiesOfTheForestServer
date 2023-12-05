using System.Net.Sockets;
using app.models;

namespace app;

public sealed class PlayerList
{
    private static List<Player> connectedPlayers;
    
    private static PlayerList playerList;

    public static PlayerList GetInstance()
    {
        if (playerList == null)
        {
            playerList = new PlayerList();
            connectedPlayers = new List<Player>();
        }

        return playerList;
    }

    public List<Player> GetList()
    {
        return connectedPlayers;
    }

    public bool HasPlayers()
    {
        return connectedPlayers.Count >= 1;
    }

    public void FindAndRemovePlayer(Socket connection)
    {
        foreach(var player in connectedPlayers.ToList())
        {
            if (player.tcpConnection.RemoteEndPoint
                .Equals(connection.RemoteEndPoint))
            {
                connectedPlayers.Remove(player);
            }
        }
    }

    public void AddPlayer(Player player)
    {
        connectedPlayers.Add(player);
    }

    public void AddUdpConnection(UdpClient udpClient)
    {
        
    }
}