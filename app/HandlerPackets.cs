using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

class HandlerPackets
{
    private static int connections = 0;
    private String data;
    private byte[] BUFFER_LENGTH = new byte[1024];
    private Socket connection;

    public HandlerPackets(Socket connection)
    {
        this.connection = connection;
    }

    public void Listening()
    {
        //connections++;
        while(RunForever())
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
        int BytesReceived = connection.Receive(BUFFER_LENGTH);

        if (HasMessageFromClient(BytesReceived))
        {
            string PacketData = ReadMessageFromClient(BytesReceived);
            Console.WriteLine("<- : {0}", PacketData);

            SendMessageToClient(PacketData);
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
        return Encoding.ASCII.GetString(BUFFER_LENGTH, 0, BytesReceived);
    }

    private void SendMessageToClient(string PacketData)
    {
        //Player PlayerObject = JsonConvert.DeserializeObject<Player>(PacketData);

        byte[] Packet = Encoding.ASCII.GetBytes(PacketData);
        connection.Send(Packet);
    }

    private void Shutdown(String command)
    {
        if (command.IndexOf("<EOF>") > -1)
        {
            connection.Close();
        }
    }
}