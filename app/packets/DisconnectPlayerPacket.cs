using System.Net.Sockets;
using System.Text;
using app;
using app.interfaces;
using app.Packets;
using Newtonsoft.Json;

class DisconnectPlayerPacket : IPacketHandler
{
    private Socket playerConnection;

    private string serializedPacket;
    private DisconnectPlayer deserializedPacket;

    public DisconnectPlayerPacket(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }

    public void Handler(string packetReceived)
    {
        var disconnectPlayerPacket 
            = new DeserializePacket().Deserialize<DisconnectPlayer>(packetReceived);

        if (disconnectPlayerPacket is { opcode: 0 })
        {
            Read(packetReceived);
            Write();

            var updateConnectedPlayers = new UpdateConnectedPlayers
            {
                opcode = 1,
                quantity = 0
            };

            new PlayerListPacket(playerConnection)
                .Handler(new SerializePacket().ObjectToString(updateConnectedPlayers));
        }
    }

    public void Read(string packetReceived)
    {
        PrintReceivedMessage();
    }
    
    public void PrintReceivedMessage()
    {
        Console.WriteLine("<- Player wants to disconnect");
    }

    public void Write()
    { 
        PrintSendedMessage();

        var disconnectPlayer = new DisconnectPlayer
        {
            opcode = 0
        };

        new IndividualPacket(playerConnection).Send(disconnectPlayer);
        DisconnectPlayer(playerConnection);

    }

    public void PrintSendedMessage()
    {
        Console.WriteLine("-> Authorization to disconnect");
    }

    private void DisconnectPlayer(Socket playerConnection)
    {
        PlayerList.GetInstance().FindAndRemovePlayer(playerConnection);
    }
    
}