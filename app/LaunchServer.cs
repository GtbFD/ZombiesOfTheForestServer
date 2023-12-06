using System.Text;
using app.server;
using app.utils.io;

class LaunchServer
{
    public static void Main(String[] args)
    {
        new ServerConnection().Config("tcp");
        new ServerConnection().Config("udp");
    }

}
