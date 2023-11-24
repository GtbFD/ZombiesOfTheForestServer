using System.Net.Sockets;
using System.Text;
using app.interfaces;
using Newtonsoft.Json;

namespace app.Packets;

public class PlayerPositionPacket : IPacketHandler
{
    private Socket playerConnection;
    private String opcode;

    private PlayerPosition playerPositionPacket;

    public PlayerPositionPacket(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }
    
    public void Handler(string packetReceived)
    {
        
        /*if (packetReceived.Contains("\"opcode\":2"))
        {
            Console.WriteLine(packetReceived);
        }*/
        /*playerPositionPacket = 
            new DeserializePacket().Deserialize<PlayerPosition>(packetReceived);

        if (playerPositionPacket is { opcode: 2 })
        {
            Read(packetReceived);
            Write();
        }*/
    }

    public void Read(string packetReceived)
    {
        PrintReceivedMessage();
    }
    
    public void PrintReceivedMessage()
    {
        Console.WriteLine("<- Player updated your position");
    }

    public void Write()
    {
        PrintSendedMessage();

        /*var playerPositionPacketWrite = new PlayerPosition
        {
            opcode = 2,
            x = playerPositionPacket.x,
            y = playerPositionPacket.y,
            z = playerPositionPacket.z
        };
        
        new BroadcastingPacket(PlayerList.GetInstance().GetList()).Send(playerPositionPacketWrite);*/
    }
    
    public void PrintSendedMessage()
    {
        Console.WriteLine("-> Sendind players position to all players");
    }
}