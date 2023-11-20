using System.Net.Sockets;

namespace app;

public sealed class ListPlayers
{
    private static List<Socket> connectedPlayers;
    
    private static ListPlayers _listPlayers;

    public static ListPlayers GetInstance()
    {
        if (_listPlayers == null)
        {
            _listPlayers = new ListPlayers();
            connectedPlayers = new List<Socket>();
        }

        return _listPlayers;
    }

    public List<Socket> GetList()
    {
        return connectedPlayers;
    }

    public void FindAndRemovePlayer(Socket player)
    {
        foreach(var connection in connectedPlayers.ToList())
        {
            if (player.RemoteEndPoint.ToString()
                .Equals(connection.RemoteEndPoint.ToString()))
            {
                connectedPlayers.Remove(connection);
            }
        }
    }

    public void AddPlayer(Socket player)
    {
        connectedPlayers.Add(player);
    }


}