using System.Net.Sockets;
using System.Text;

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
        opcode = new Opcode(packetReceived).GetOpcode();

        if (opcode.Equals("0001"))
        {
            Read(packetReceived);
            Write();
        }

    }
    
    public override void Read(string packetReceived)
    {
        PrintReceivedMessage();
        opcode = new Opcode(packetReceived).GetOpcode();
    }
    
    public override void PrintReceivedMessage()
    {
        Console.WriteLine("<- Player wants to read players list");
    }

    public override void Write()
    {
        PrintSendedMessage();
    
        var quantityPlayers = "0001" + ListPlayers.GetInstance().GetList().Count;
        
        var quantityPlayersConnected = Encoding.ASCII.GetBytes(quantityPlayers);
        new BroadcastingPacket(ListPlayers.GetInstance().GetList()).Send(quantityPlayersConnected);
    }
    
    public override void PrintSendedMessage()
    {
        Console.WriteLine("-> Sendind players list to all players");
    }
}