using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace app.Packets;

public class PlayerPositionPacket : Packet
{
    private byte[] BUFFER = new byte[1024];
    private Socket playerConnection;
    private String opcode;

    private PlayerPosition playerPosition;

    public PlayerPositionPacket(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
        this.playerPosition = new PlayerPosition();
    }
    
    public override void Handler(string packetReceived)
    {
        var playerPositionPacket = JsonConvert.DeserializeObject<PlayerPosition>(packetReceived);

        if (playerPositionPacket.opcode == 2)
        {
            Console.WriteLine("Opcode: " + playerPositionPacket.opcode + " [X: " + playerPositionPacket.x + ", Y: " 
                              + playerPositionPacket.y + ", Z: " + playerPositionPacket.z + "]");
        }
        /*opcode = new Opcode(packetReceived).GetOpcode();
        if (opcode.Contains("0002"))
        {
            Read(packetReceived);
            //Write();
            
        }*/
    }
    
    public override void Read(string packetReceived)
    {
        PrintReceivedMessage();
        //opcode = new Opcode(packetReceived).GetOpcode();
        
    }
    
    public override void PrintReceivedMessage()
    {
        Console.WriteLine("<- Player updated your position");
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