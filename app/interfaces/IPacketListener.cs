using System.Net.Sockets;

namespace app.interfaces;

public interface IPacketListener
{
    public void SetConnection(Socket connection);
    public void ListenToPackets();
}