﻿using System.Net.Sockets;
using app.packets.enums;
using app.utils.io;

namespace app.handlers;

public class LoginPlayerHandler : IPacketHandler
{

    private Socket connection;

    public LoginPlayerHandler(Socket connection)
    {
        this.connection = connection;
    }
    
    public void Read(byte[] packetReceived)
    {
        var reader = new ReadPacket(packetReceived);

        var opcode = reader.ReadInt();
        
        if (opcode == (int)OpcodePackets.LOGIN_PLAYER)
        {
            Console.WriteLine("[LOGIN] <- PACKET_RECEIVED - ID: " + opcode);
            Write();
        }
    }

    public void Write()
    {
        var writer = new WritePacket();
        writer.Write((int)OpcodePackets.LOGIN_PLAYER_RESPONSE_SUCCESS);
        var packet = writer.BuildPacket();
        new IndividualPacket(connection).Send(packet);

        var writerUpdateConnections = new WritePacket();
        writerUpdateConnections.Write((int) OpcodePackets.UPDATE_CONNECTIONS_RESPONSE);
        writerUpdateConnections.Write(PlayerList.GetInstance().GetList().Count);

        var packetUpdateConnections = writerUpdateConnections.BuildPacket();
        new BroadcastingPacket(connection, 
            PlayerList.GetInstance().GetList()).SendAll(packetUpdateConnections);
    }

    public void Handler(byte[] packetReceived)
    {
        Read(packetReceived);
    }
}