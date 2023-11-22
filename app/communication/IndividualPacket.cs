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
    
    public void Send(IPacket data)
    {
        SerializePacket serializePacket = new SerializePacket();
        string packet = serializePacket.ObjectToString(data);
        playerConnection.Send(new SerializePacket().Serialize(packet));
    }
}