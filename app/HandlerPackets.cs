using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

class HandlerPackets
{
    private static int connections = 0;
    private String data;
    private byte[] BUFFER_LENGTH;
    private Socket PlayerConnection;

    private List<Socket> ConnectedPlayers;

    public HandlerPackets(Socket Connection, List<Socket> ConnectedPlayers)
    {
        this.BUFFER_LENGTH = new byte[1024];
        this.ConnectedPlayers = ConnectedPlayers;
        this.PlayerConnection = Connection;
    }

    public void Listening()
    {
        Console.WriteLine("- [STATUS]: {0} connected players", ConnectedPlayers.Count);
        while (RunForever())
        {
            ReadMessagesFromClient();
        }
        
    }

    private bool RunForever()
    {
        return true;
    }

    private void ReadMessagesFromClient()
    {
        int BytesReceived = PlayerConnection.Receive(BUFFER_LENGTH);

        if (HasMessageFromClient(BytesReceived))
        {
            string PacketData = ReadMessageFromClient(BytesReceived);
            
            if (PacketData.IndexOf("<EOF>") != -1)
            {
                Console.WriteLine("<- {0} wants to leave", PlayerConnection.RemoteEndPoint);
                byte[] CommandToLeave = Encoding.ASCII.GetBytes("<EOF>");
                Console.WriteLine("-> {0} authorization to leave", PlayerConnection.RemoteEndPoint);
                PlayerConnection.Send(CommandToLeave);

                DisconnectPlayer(PlayerConnection);
            }
            else
            {
                byte[] c = Encoding.ASCII.GetBytes("Hello World");
                Console.WriteLine("-> New message to player");
                PlayerConnection.Send(c);
            }

            
        }

    }

    private bool HasMessageFromClient(int BytesReceived)
    {
        if (BytesReceived != 0)
        {
            return true;
        }
        return false;
    }

    private String ReadMessageFromClient(int BytesReceived)
    {
        if (HasMessageFromClient(BytesReceived))
        {
            return Encoding.ASCII.GetString(BUFFER_LENGTH, 0, BytesReceived);
        }
        return null;
    }

    private void SendMessageToClient(string PacketData)
    {
        //Player PlayerObject = JsonConvert.DeserializeObject<Player>(PacketData);

        byte[] Packet = Encoding.ASCII.GetBytes(PacketData);
        PlayerConnection.Send(Packet);
    }

    private void DisconnectPlayer(Socket PlayerConnection)
    {
        foreach(Socket player in ConnectedPlayers.ToList())
        {
            FindAndRemovePlayer(player);
        }
    }

    private void FindAndRemovePlayer(Socket player)
    {
        if (ConnectedPlayers.Contains(player))
        {
            ConnectedPlayers.Remove(player);
        }
    }
}