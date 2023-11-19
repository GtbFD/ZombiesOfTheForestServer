using System.Net.Sockets;
using System.Text;
using app;
using app.Packets;

class DisconnectPlayerPacket : Packet
{
    private byte[] BUFFER = new byte[1024];
    private Socket playerConnection;
    private String opcode;
    
    private List<Socket> connectedPlayers;

    public DisconnectPlayerPacket(Socket playerConnection, List<Socket> connectedPlayers)
    {
        this.playerConnection = playerConnection;
        this.connectedPlayers = connectedPlayers;
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
        
        DisconnectPlayer(playerConnection);
        var commandToLeave = Encoding.ASCII.GetBytes("0000");
        
        new IndividualPacket(playerConnection).Send(commandToLeave);

    }

    public override void PrintSendedMessage()
    {
        Console.WriteLine("-> Authorization to disconnect");
    }
    
    private void DisconnectPlayer(Socket playerConnection)
    {
        foreach(var player in connectedPlayers.ToList())
        {
            FindAndRemovePlayer(playerConnection);
        }
    }
    
    private void FindAndRemovePlayer(Socket playerConnection)
    {
        foreach(var connection in connectedPlayers.ToList())
        {
            if (!playerConnection.RemoteEndPoint.ToString()
                    .Equals(connection.RemoteEndPoint.ToString())) continue;
            connectedPlayers.Remove(connection);
            break;
        }
    }
}