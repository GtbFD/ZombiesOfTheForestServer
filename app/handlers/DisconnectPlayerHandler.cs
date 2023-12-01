using System.Net.Sockets;
using app.packets.enums;
using app.utils.io;
namespace app.handlers;

class DisconnectPlayerHandler : IPacketHandler
{
    private Socket playerConnection;

    public DisconnectPlayerHandler(Socket playerConnection)
    {
        this.playerConnection = playerConnection;
    }

    public void Handler(byte[] packetReceived)
    {

        Read(packetReceived);
    }

    public void Read(byte[] packetReceived)
    {
        var reader = new ReadPacket(packetReceived);
        var opcode = reader.ReadInt();

        if (opcode == (int)OpcodePackets.DISCONNECT_PLAYER)
        {
            Console.WriteLine("[DISCONNECT] <- PACKET_RECEIVED - ID: " + opcode);
            Console.WriteLine("Player {0} disconnected", playerConnection.RemoteEndPoint);
            DisconnectPlayer(playerConnection);
            Write();
        }
    }
    

    public void Write()
    {
        var writer = new WritePacket();
        writer.Write((int) OpcodePackets.DISCONNECT_PLAYER_RESPONSE);
        var packet = writer.BuildPacket();
        
        new IndividualPacket(playerConnection).Send(packet);
        
        var writerUpdateConnections = new WritePacket();
        writerUpdateConnections.Write((int) OpcodePackets.UPDATE_CONNECTIONS_RESPONSE);
        writerUpdateConnections.Write(PlayerList.GetInstance().GetList().Count);

        var packetUpdateConnections = writerUpdateConnections.BuildPacket();
        new BroadcastingPacket(null, 
            PlayerList.GetInstance().GetList()).SendAll(packetUpdateConnections);
    }
    

    private void DisconnectPlayer(Socket connection)
    {
        PlayerList.GetInstance().FindAndRemovePlayer(connection);
    }
    
}