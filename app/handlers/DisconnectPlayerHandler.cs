using System.Net.Sockets;
using app.interfaces;
using app.packets;
using app.packets.enums;
using app.packets.request;
using app.packets.response;
using app.utils.identifier;
using app.utils.io;

namespace app.handlers;

class DisconnectPlayerHandler : IPacketHandler
{
    private Socket playerConnection;

    public DisconnectPlayerHandler(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }

    public void Handler(string packetReceived)
    {

        Read(packetReceived);
    }

    public void Read(string packetReceived)
    {
        var opcode = PacketIdentifier.Opcode((string) packetReceived);
        
        if (opcode == (int)OpcodePackets.DISCONNECT_PLAYER)
        {
            
            Console.WriteLine("Player {0} disconnected", playerConnection.RemoteEndPoint);
            DisconnectPlayer(playerConnection);
        }
    }
    

    public void Write(string packetReceived)
    { 
        PrintSendedMessage();

        var disconnectPlayer = new DisconnectPlayerResponsePacket()
        {
            opcode = (int)OpcodePackets.DISCONNECT_PLAYER_RESPONSE
        };

        new IndividualPacket(playerConnection).Send(disconnectPlayer);
        DisconnectPlayer(playerConnection);
    }

    public void PrintSendedMessage()
    {
        Console.WriteLine("-> Authorization to disconnect");
    }

    private void DisconnectPlayer(Socket connection)
    {
        PlayerList.GetInstance().FindAndRemovePlayer(connection);
    }
    
}