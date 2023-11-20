using System.Net.Sockets;
using System.Text;
using app;
using app.Packets;

class DisconnectPlayerPacket : Packet
{
    private byte[] BUFFER = new byte[1024];
    private Socket playerConnection;
    private String opcode;

    public DisconnectPlayerPacket(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }

    public override void Handler(string packetReceived)
    {
        opcode = new Opcode(packetReceived).GetOpcode();

        if (opcode.Equals("0000"))
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
        Console.WriteLine("<- Player wants to disconnect");
    }

    public override void Write()
    { 
        PrintSendedMessage();
        
        var commandToLeave = Encoding.ASCII.GetBytes("0000");
        
        new IndividualPacket(playerConnection).Send(commandToLeave);
        DisconnectPlayer(playerConnection);

    }

    public override void PrintSendedMessage()
    {
        Console.WriteLine("-> Authorization to disconnect");
    }
    
    private void DisconnectPlayer(Socket playerConnection)
    {
        ListPlayers.GetInstance().FindAndRemovePlayer(playerConnection);
    }
    
}