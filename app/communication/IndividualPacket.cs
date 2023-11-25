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
    
    public void Send(IPacket data)
    {
        playerConnection.Send(SerializePacket.Serialize(data));
    }
}