using System.Net.Sockets;

namespace app;

public sealed class PlayerList
{
    private static List<Socket> connectedPlayers;
    
    private static PlayerList _playerList;

    public static PlayerList GetInstance()
    {
        if (_playerList == null)
        {
            _playerList = new PlayerList();
            connectedPlayers = new List<Socket>();
        }

        return _playerList;
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