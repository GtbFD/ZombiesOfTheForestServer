using System.Net.Sockets;
using System.Text;

namespace app.Packets;

public class PlayerListPacket : Packet
{
    private byte[] BUFFER = new byte[1024];
    private Socket playerConnection;
    private String opcode;
    
    private List<Socket> connectedPlayers;
    
    public PlayerListPacket(Socket playerConnection, List<Socket> connectedPlayers)
    {
        this.playerConnection = playerConnection;
        this.connectedPlayers = connectedPlayers;
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
    
        var quantityPlayers = "" + connectedPlayers.Count;
        
        var quantityPlayersConnected = Encoding.ASCII.GetBytes("0001");
        new BroadcastingPacket(connectedPlayers).Send(quantityPlayersConnected);
    }
    
    public override void PrintSendedMessage()
    {
        Console.WriteLine("-> Sendind players list to all players");
    }
}