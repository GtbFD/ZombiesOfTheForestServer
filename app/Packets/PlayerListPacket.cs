using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace app.Packets;

public class PlayerListPacket : Packet
{
    private byte[] BUFFER = new byte[1024];
    private Socket playerConnection;
    private String opcode;

    public PlayerListPacket(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }
    
    public override void Handler(string packetReceived)
    {
        var updateConnectedPlayersPacket = JsonConvert.DeserializeObject<UpdateConnectedPlayers>(packetReceived);

        if (updateConnectedPlayersPacket.opcode == 1)
        {
            Read(packetReceived);
            Write();
        }

    }
    
    public override void Read(string packetReceived)
    {
        PrintReceivedMessage();
    }
    
    public override void PrintReceivedMessage()
    {
        Console.WriteLine("<- Player wants to read players list");
    }

    public override void Write()
    {
        PrintSendedMessage();

        var updateConnectedPlayers = new UpdateConnectedPlayers();
        updateConnectedPlayers.opcode = 1;
        updateConnectedPlayers.quantity = ListPlayers.GetInstance().GetList().Count;

        var updateConnectedPlayersSerialized = JsonConvert.SerializeObject(updateConnectedPlayers);
        var updateConnectedPlayersPacket = Encoding.ASCII.GetBytes(updateConnectedPlayersSerialized);
        
        new BroadcastingPacket(ListPlayers.GetInstance().GetList()).Send(updateConnectedPlayersPacket);
    }
    
    public override void PrintSendedMessage()
    {
        Console.WriteLine("-> Sendind players list to all players");
    }
}