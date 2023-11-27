using System.Net.Sockets;

namespace app.models;

public class Player
{
    public Socket connection { get; set; }
    public Localization localization { get; set; }
}