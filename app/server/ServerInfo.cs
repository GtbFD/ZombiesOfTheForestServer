﻿using System.Net;
using System.Net.Sockets;

namespace app.server;

public class ServerInfo
{
    private string host;
    private int port;
    private Socket connection;
    private byte[] buffer;

    public ServerInfo()
    {
        host = "localhost";
        port = 11000;
        buffer = new byte[1024];
    }

    public string GetHost()
    {
        return host;
    }

    public int GetPort()
    {
        return port;
    }

    public byte[] GetBuffer()
    {
        return buffer;
    }

    public string RemoteEndPoint(Socket connection)
    {
        return connection.RemoteEndPoint.ToString();
    }

    public IPAddress IPAdress()
    {
        return Dns.GetHostEntry(GetHost()).AddressList[0];
    }

    public AddressFamily AddressFamily()
    {
        return IPAdress().AddressFamily;
    }
}