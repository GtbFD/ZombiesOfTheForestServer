using System.Net.Sockets;
using app.interfaces;
using app.utils.io;

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