using System.Net;

namespace app.server;

public class EndPointServer
{

    
    public IPEndPoint Config(string ipAdress, int port)
    {
        var endPointInfo = new ServerInfo();

        var ip = endPointInfo.IPAdress(ipAdress);
        
        var endPoint = new IPEndPoint(ip, port);

        return endPoint;
    }
}