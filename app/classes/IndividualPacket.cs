using System.Net.Sockets;
using app.interfaces;

namespace app;

public class IndividualPacket : ISendMessage
{

    private Socket playerConnection;

    public IndividualPacket(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }
    
    public void Send(byte[] data)
    {
        playerConnection.Send(data);
    }
}