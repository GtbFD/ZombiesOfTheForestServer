using System.Net.Sockets;
using System.Text;
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

    public void Handler(String packetReceived)
    {
        opcode = new Opcode(packetReceived).GetOpcode();

        if (!opcode.Equals("0000")) return;
        
        Read(packetReceived);
        Write();
    }

    public override void Read(String packetReceived)
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
        playerConnection.Send(commandToLeave);

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
    
    public void FindAndRemovePlayer(Socket playerConnection)
    {
        foreach(var Connection in connectedPlayers.ToList())
        {
            if (playerConnection.RemoteEndPoint.ToString()
                .Equals(Connection.RemoteEndPoint.ToString()))
            {
                connectedPlayers.Remove(Connection);
                break;
            }
        }
    }
}