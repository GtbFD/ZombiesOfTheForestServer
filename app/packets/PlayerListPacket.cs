using System.Net.Sockets;
using System.Text;
using app.interfaces;
using Newtonsoft.Json;

namespace app.Packets;

public class PlayerListPacket : IPacketHandler
{
    private Socket playerConnection;
    private String opcode;

    public PlayerListPacket(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }
    
    public void Handler(string packetReceived)
    {
        var updateConnectedPlayersPacket = 
            new DeserializePacket().Deserialize<UpdateConnectedPlayers>(packetReceived);

        if (updateConnectedPlayersPacket != null && updateConnectedPlayersPacket.opcode == 1)
        {
            Read(packetReceived);
            Write();
        }

    }

    public void Serialize()
    {
        
    }

    public void Deserialize()
    {
        
    }

    public void Read(string packetReceived)
    {
        PrintReceivedMessage();
    }
    
    public void PrintReceivedMessage()
    {
        Console.WriteLine("<- Player wants to read players list");
    }

    public void Write()
    {
        PrintSendedMessage();

        var updateConnectedPlayers = new UpdateConnectedPlayers
        {
            opcode = 1,
            quantity = ListPlayers.GetInstance().GetList().Count
        };
        
        
        new BroadcastingPacket(ListPlayers.GetInstance().GetList()).Send(updateConnectedPlayers);
    }
    
    public void PrintSendedMessage()
    {
        Console.WriteLine("-> Sendind players list to all players");
    }
}